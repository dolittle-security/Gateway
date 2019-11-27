import * as express from 'express';
import * as http from 'http';
import * as querystring from 'querystring';
import { Express, Request, Response } from 'express';
import { Configuration, Portal } from "../Configuration";
import { ParamsDictionary } from 'express-serve-static-core';

async function PrepareRequestToGateway(req: Request<ParamsDictionary>, res: Response, portal: Portal, path: string, responseCallback: (response: http.IncomingMessage) => void): Promise<http.ClientRequest>
{
    const headers = req.headers;
    headers['Owner-Tenant-ID'] = portal.Owner.toString();
    headers['Portal-ID'] = portal.Id.toString();
    headers['Portal-Domain'] = portal.Domain;
    headers['X-Forwarded-Host'] = req.headers.host;

    const query = querystring.stringify(req.query);
    const proxyRequest = http.request({
        host: 'localhost',
        port: 5000,
        path: path + (query ? '?'+query : ''),
        method: req.method,
        headers: headers,
    }, responseCallback).on('error', (error) => {
        console.log(`Error forwarding request to proxy ${error.message}`);
        res.status(500);
        res.end();
    });
    return proxyRequest;
}

async function ForwardRequestToGateway(req: Request<ParamsDictionary>, res: Response, portal: Portal, rewritePath: (path: string) => string)
{
    const proxyRequest = await PrepareRequestToGateway(req, res, portal, rewritePath(req.path), (proxyResponse) => {
        const chunks = [];
        proxyResponse.on('data', (chunk) => {
            chunks.push(chunk);
        });
        proxyResponse.on('end', () => {
            const data = Buffer.concat(chunks, parseInt(proxyResponse.headers["content-length"]));
            res.set(proxyResponse.headers);
            res.status(proxyResponse.statusCode);
            res.end(data);
        });
        proxyResponse.on('error', (error) => {
            console.log(`Error forwarding request to proxy ${error.message}`);
            res.status(500);
            res.end();
        });
    });
    req.on('data', (chunk) => {
        proxyRequest.write(chunk);
    });
    req.on('end', () => {
        proxyRequest.end();
    });
}

class AuthSubrequestResult
{
    public StatusCode: number;
    public TenantID: string;
    public Claims: any;
}

function GetClaims(response: http.IncomingMessage): any
{
    const claims = {};
    const claimsHeader = response.headers['claims'] as string;
    for (const claimTypeValue of claimsHeader.split(',')) {
        const [type, value] = claimTypeValue.split('=', 2);
        claims[type] = value;
    }
    return claims;
}

async function PerformAuthSubRequest(req: Request<ParamsDictionary>, res: Response, portal: Portal): Promise<AuthSubrequestResult>
{
    return new Promise(async (resolve) => {
        console.log(`Performing auth subrequest`);
        const proxyRequest = await PrepareRequestToGateway(req, res, portal, '/auth', (proxyResponse) => {
            resolve({
                StatusCode: proxyResponse.statusCode,
                TenantID: proxyResponse.headers['tenant-id'] as string,
                Claims: GetClaims(proxyResponse),
            });
        });
        proxyRequest.end();
    });

}

async function HandlePortalRequest(req: Request<ParamsDictionary>, res: Response, portal: Portal)
{
    if (req.path.startsWith('/.well-known') || req.path.startsWith('/connect') || req.path.startsWith('/signin') || req.path.startsWith('signout') || req.path.startsWith('/dolittle/security/gateway'))
    {
        await ForwardRequestToGateway(req, res, portal, (path) => path);
    }
    else if (req.path.startsWith('/api/dolittle/security/gateway'))
    {
        await ForwardRequestToGateway(req, res, portal, (path) => path.replace('/api/dolittle/security/gateway', '/api'));
    }
    else
    {
        const authResponse = await PerformAuthSubRequest(req, res, portal);
        if (authResponse.StatusCode == 401 || authResponse.StatusCode == 403)
        {
            res.redirect(302, `/signin?rd=${encodeURIComponent(req.path)}`);
            res.end();
        }
        else
        {
            res.status(200);
            res.write(`Welcome to portal ${portal.DisplayName}\n`);
            res.write(`You are signed in to tenant ${authResponse.TenantID}\n`);
            res.write(`Your claims are:\n`);
            res.end(JSON.stringify(authResponse.Claims));
        }
    }
}

export function CreateProxy(configuration: Configuration): Express
{
    const server = express();

    for (const portal of configuration.Portals) {
        server.use(async (req, res, next) => {
            if (req.hostname.endsWith(portal.Domain))
            {
                console.log(`Handling request for portal ${portal.DisplayName} to '${req.path}'`);
                await HandlePortalRequest(req, res, portal);
            }
            else
            {
                next();
            }
        });
    }    
    
    return server;
}