/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;
using Context;
using Dolittle.Execution;
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
            identity.AddClaim("iss",$"https://{issuer}/");

            identity.AddClaim("sub", user.Id.ToString());

            identity.AddClaim("tid", _tenantManager.Current.Tenant.ToString());

            identity.AddClaim("iat", _clock.UtcNow.ToUnixTimeSeconds().ToString());

            // TODO: Add any additional claims the principal should have based on portal configuration

            return new ClaimsPrincipal(identity);
        }
    }
}