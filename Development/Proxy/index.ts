import * as express from 'express';
import * as http from 'http';
import { ClientRequest, IncomingMessage } from 'http';
import * as querystring from 'querystring';
import * as proxy from 'http-proxy-middleware';
import { Express, Request, Response } from 'express';
import { Configuration, Portal } from "../Configuration";

function FindPortalFromRequest(req: Request, configuration: Configuration): Portal
{
    for (const portal of configuration.Portals)
    {
        if (req.hostname.endsWith(portal.Domain))
        {
            return portal;
        }
    }
    throw `No portal found for host ${req.hostname}`;
}

function AddHeadersToProxyRequest(req: Request, proxyReq: ClientRequest, configuration: Configuration): void
{
    const portal = FindPortalFromRequest(req, configuration);
    proxyReq.setHeader('Owner-Tenant-ID', portal.Owner.toString());
    proxyReq.setHeader('Portal-ID', portal.Id.toString());
    proxyReq.setHeader('Portal-Domain', portal.Domain);
    proxyReq.setHeader('X-Forwarded-Host', req.headers.host);
}

class AuthSubrequestResult
{
    public StatusCode: number;
    public TenantID: string;
    public Claims: any;
}

function GetClaims(response: IncomingMessage): any
{
    const claims = {};
    const claimsHeader = response.headers['claims'] as string;
    for (const claimTypeValue of claimsHeader.split(',')) {
        const [type, value] = claimTypeValue.split('=', 2);
        claims[type] = value;
    }
    return claims;
}

async function PerformAuthSubrequest(req: Request, configuration: Configuration): Promise<AuthSubrequestResult>
{
    return new Promise<AuthSubrequestResult>((resolve, reject) => {
        const authRequest = http.request({
            hostname: 'localhost',
            port: 5000,
            path: '/auth',
            method: 'GET',
            headers: req.headers,
        });
        AddHeadersToProxyRequest(req, authRequest, configuration);
        authRequest.on('response', (authResponse) => {
            const result: AuthSubrequestResult =
            {
                StatusCode: authResponse.statusCode,
                TenantID: undefined,
                Claims: undefined,
            };
            if (authResponse.statusCode == 200)
            {
                result.TenantID = authResponse.headers['tenant-id'] as string;
                result.Claims = GetClaims(authResponse);
            }
            resolve(result);
        });
        authRequest.on('error', (error) => {
            reject(error);
        });
        authRequest.end();
    })
}

function CreateNormalPageHandler(configuration: Configuration): (req: Request, res: Response) => void
{
    return async (req, res) => {
        const authResponse = await PerformAuthSubrequest(req, configuration);
        console.log('AuthResponse status', authResponse.StatusCode);
        if (authResponse.StatusCode == 401 || authResponse.StatusCode == 403)
        {
            let redirectUri = req.path;
            let redirectQuery = querystring.stringify(req.params);
            if (redirectQuery)
            {
                redirectUri += '?'+redirectQuery;
            }
            res.redirect(`/signin?rd=${encodeURIComponent(redirectUri)}`)
        }
        else
        {
            const portal = FindPortalFromRequest(req, configuration);
            res.status(200);
            res.write(`Welcome to portal ${portal.DisplayName}\n`);
            res.write(`You are signed in to tenant ${authResponse.TenantID}\n`);
            res.write(`Your claims are:\n`);
            res.end(JSON.stringify(authResponse.Claims));
        }
    };
}

export function CreateProxy(configuration: Configuration): Express
{
    const server = express();

    const forwarder = proxy({
        target: 'http://localhost:5000',
        ws: true,
        pathRewrite: {
            '^/api/dolittle/security/gateway': '/api',
        },
        onProxyReq: (proxyReq: ClientRequest, req: Request) => {
            console.log('Forwarding for', req.path);
            AddHeadersToProxyRequest(req, proxyReq, configuration);
        },
    });

    const handler = CreateNormalPageHandler(configuration);
    
    server.use('/signin', forwarder);
    server.use('/signout', forwarder);
    server.use('/dolittle/security/gateway', forwarder);
    server.use('/api/dolittle/security/gateway', forwarder);
    server.use('/.well-known', forwarder);
    server.use('/connect', forwarder);
    server.use(handler);

    return server;
}