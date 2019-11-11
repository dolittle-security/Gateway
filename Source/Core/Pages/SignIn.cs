/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Dolittle.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Read.Providers.Choosing;

namespace Core.Pages
{
    [Route("SignIn")]
    public class SignIn : ControllerBase
    {
        readonly ICanResolveProvidersForChoosing _resolver;
        readonly IAuthenticationFrontend _frontend;
        readonly ILogger _logger;

        public SignIn(ICanResolveProvidersForChoosing resolver, IAuthenticationFrontend frontend, ILogger logger)
        {
            _resolver = resolver;
            _frontend = frontend;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Signin(Uri rd)
        {
            var providers = _resolver.AllAvailableIdentityProvidersForChoosing();
            switch (providers.Count())
            {
                case 0:
                return _frontend.NoProvidersAvailable(HttpContext);

                case 1:
                return Challenge(new AuthenticationProperties {
                    RedirectUri = rd.ToString(),
                }, providers.First().Id.ToString());

                default:
                return _frontend.ChooseProvider(HttpContext);
            }
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            return _frontend.Error(HttpContext);
        }
    }
}