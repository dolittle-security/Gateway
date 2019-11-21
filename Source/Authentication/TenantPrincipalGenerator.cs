/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;
using Context;
using Dolittle.Execution;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Providers.Claims;
using Read.Users;

namespace Authentication
{
    public class TenantPrincipalGenerator : ICanGenerateTenantPrincipal
    {
        readonly IPortalContextManager _portalManager;
        readonly IExecutionContextManager _tenantManager;
        readonly ISystemClock _clock;

        public TenantPrincipalGenerator(IPortalContextManager portalManager, IExecutionContextManager tenantManager, ISystemClock clock)
        {
            _portalManager = portalManager;
            _tenantManager = tenantManager;
            _clock = clock;
        }

        public ClaimsPrincipal GenerateFor(User user)
        {
            var identity = new ClaimsIdentity(Constants.IdentityAuthenticationType);

            var issuer = _portalManager.Current.BaseDomain;
            if (!string.IsNullOrWhiteSpace(_portalManager.Current.SubDomain))
            {
                issuer = _portalManager.Current.SubDomain+"."+issuer;
            }
            identity.AddClaim(JwtClaimTypes.Issuer, $"https://{issuer}"); // TODO: How do we sync this with the IdentityServer "GetIdentityServerIssuerUri()" ?

            identity.AddClaim(JwtClaimTypes.Subject, user.Id.ToString());

            identity.AddClaim("tid", _tenantManager.Current.Tenant.ToString());

            identity.AddClaim(new Claim(JwtClaimTypes.IssuedAt, _clock.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer));

            // TODO: Add any additional claims the principal should have based on portal configuration

            return new ClaimsPrincipal(identity);
        }
    }
}