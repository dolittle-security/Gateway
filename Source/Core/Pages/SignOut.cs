/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Authentication;
using Authentication.Frontend;
using Concepts.Claims;
using Concepts.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Core.Pages
{
    [Route("SignOut")]
    public class SignOut : ControllerBase
    {
        readonly IAuthenticationFrontend _frontend;

        public SignOut(IAuthenticationFrontend frontend)
        {
            _frontend = frontend;
        }

        [HttpGet]
        public async Task<IActionResult> Signout(string rd)
        {
            var authResult = await HttpContext.AuthenticateAsync(Constants.ExternalCookieSchemeName);
            await HttpContext.SignOutAsync(Constants.InternalCookieSchemeName);
            await HttpContext.SignOutAsync(Constants.ExternalCookieSchemeName);
            if (authResult.Succeeded)
            {
                if (authResult.Principal.TryGetProviderSubjectClaims(out var provider, out var subject) && Guid.TryParse(provider, out var providerId))
                {
                    // TODO: Check that it is actually a provider?
                    return _frontend.SignedOut(HttpContext, providerId, rd);
                }
            }
            if (!string.IsNullOrWhiteSpace(rd))
            {
                return Redirect(rd);
            }
            return _frontend.SignedOut(HttpContext);
        }

        [HttpGet("External")]
        public IActionResult External(IdentityProviderId idp)
        {
            return _frontend.SignOutExternalProvider(HttpContext, idp);
        }
    }
}