/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication;
using Concepts.Claims;
using Dolittle.Tenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Read.Portals.UserMapping;

namespace Core.Endpoints.Api
{
    [Route("api/Dolittle/Sentry/Gateway/Tenants")]
    public class Tenants : ControllerBase
    {
        readonly ICanResolveTenantsForProviderSubjects _mapper;

        public Tenants(ICanResolveTenantsForProviderSubjects mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantId>>> AllTenants()
        {
            var authResult = await HttpContext.AuthenticateAsync(Constants.ExternalCookieSchemeName);
            if (authResult.Succeeded)
            {
                if (authResult.Principal.TryGetIssuerSubjectClaims(out var issuer, out var subject))
                {
                    return new ActionResult<IEnumerable<TenantId>>(_mapper.GetTenantsFor(issuer, subject));
                }
            }
            return Unauthorized();
        }
    }
}