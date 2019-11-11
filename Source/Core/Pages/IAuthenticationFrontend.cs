/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Pages
{
    public interface IAuthenticationFrontend
    {
        IActionResult NoProvidersAvailable(HttpContext context);

        IActionResult ChooseProvider(HttpContext context);

        IActionResult SpecifiedProviderDoesNotExist(HttpContext context, IdentityProviderId provider);
        IActionResult Error(HttpContext context);
    }
}