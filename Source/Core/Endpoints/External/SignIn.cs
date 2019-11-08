/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Core.Pages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Read.Providers.Choosing;

namespace Core.Endpoints.External
{
    [Route("Dolittle/Sentry/Gateway/SignIn")]
    public class SignIn : ControllerBase
    {
        readonly ICanResolveProvidersForChoosing _resolver;
        readonly IAuthenticationFrontend _frontend;

        public SignIn(ICanResolveProvidersForChoosing resolver, IAuthenticationFrontend frontend)
        {
            _resolver = resolver;
            _frontend = frontend;
        }

        [HttpGet]
        public IActionResult Signin(Uri rd, Guid ip)
        {
            var providers = _resolver.AllAvailableIdentityProvidersForChoosing();
            if (providers.Where(_ => _.Id.Value == ip).Any())
            {
                return Challenge(new AuthenticationProperties {
                    RedirectUri = rd.ToString(),
                }, ip.ToString());
            }
            else
            {
                return _frontend.SpecifiedProviderDoesNotExist(HttpContext, ip);
            }
        }
    }
}