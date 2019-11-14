/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Frontend
{
    public interface IAuthenticationFrontend
    {
        IActionResult ChooseProvider(HttpContext context);
        IActionResult ChooseTenant(HttpContext context);
        IActionResult SignedOut(HttpContext context);
        IActionResult SignedOut(HttpContext context, IdentityProviderId provider, string redirect);
        IActionResult SignOutExternalProvider(HttpContext context, IdentityProviderId provider);


        IActionResult NoProvidersAvailable(HttpContext context);
        IActionResult NoTenantsForUser(HttpContext context);
        IActionResult SpecifiedProviderDoesNotExist(HttpContext context, IdentityProviderId provider);
        IActionResult Error(HttpContext context);
    }
}