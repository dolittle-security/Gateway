/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Concepts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Context
{
    public class PortalContextMiddelware
    {
        const string PortalIdHeaderName = "Portal-ID";
        const string PortalBaseDomainHeaderName = "Portal-Domain";

        readonly RequestDelegate _next;
        readonly IPortalContextManager _contextManager;

        public PortalContextMiddelware(RequestDelegate next, IPortalContextManager contextManager)
        {
            _next = next;
            _contextManager = contextManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var portalId = GetRequiredPortalId(context.Request.Headers);
            var domain = context.Request.Host.Host;
            ThrowIfDomainIsNotAValidHostName(domain);
            var baseDomain = GetRequiredBaseDomain(context.Request.Headers);
            ThrowIfBaseDomainIsNotSuffixOfDomain(domain, baseDomain);

            var subDomain = "";
            if (domain.Length > baseDomain.Length)
            {
                subDomain = domain.Substring(0, baseDomain.Length-3);
            }

            _contextManager.SetPortal(portalId);
            _contextManager.SetBaseDomain(baseDomain);
            _contextManager.SetSubDomain(subDomain);

            await _next(context);
        }

        PortalId GetRequiredPortalId(IDictionary<string, StringValues> headers)
        {
            var portalIdString = GetSingleHeaderValue(headers, PortalIdHeaderName);
            if (Guid.TryParse(portalIdString, out var portalIdGuid))
            {
                return portalIdGuid;
            }
            else
            {
                throw new PortalIdHeaderIsNotAValidGuid(PortalIdHeaderName, portalIdString);
            }
        }

        string GetRequiredBaseDomain(IDictionary<string,StringValues> headers)
        {
            var baseDomain = GetSingleHeaderValue(headers, PortalBaseDomainHeaderName);
            if (Uri.CheckHostName(baseDomain) == UriHostNameType.Dns)
            {
                return baseDomain;
            }
            else
            {
                throw new PortalBaseDomainHeaderIsNotAValidHostName(PortalBaseDomainHeaderName, baseDomain);
            }
        }

        string GetSingleHeaderValue(IDictionary<string, StringValues> headers, string headerName)
        {
            if (!headers.ContainsKey(headerName)) throw new SingletonHeaderHasNoValueSet(headerName);
            var values = headers[headerName];
            if (values.Count < 1) throw new SingletonHeaderHasNoValueSet(headerName);
            if (values.Count > 1) throw new SingletonHeaderHasMultipleValuesSet(headerName, values);
            var value = values[0];
            if (string.IsNullOrWhiteSpace(value)) throw new SingletonHeaderHasNoValueSet(headerName);
            return value;
        }

        void ThrowIfBaseDomainIsNotSuffixOfDomain(string domain, string baseDomain)
        {
            if (!domain.EndsWith(baseDomain, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new BaseDomainIsNotSuffixOfDomain(baseDomain, domain);
            }
        }

        private void ThrowIfDomainIsNotAValidHostName(string domain)
        {
            if (Uri.CheckHostName(domain) != UriHostNameType.Dns)
            {
                throw new DomainIsNotAValidHostName(domain);
            }
        }
    }
}