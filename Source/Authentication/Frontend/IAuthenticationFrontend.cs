/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Providers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Frontend
{
    public interface IAuthenticationFrontend
    {
        IActionResult ChooseProvider(HttpContext context);
        IActionResult ChooseTenant(HttpContext context);
        IActionResult NoProvidersAvailable(HttpContext context);
        IActionResult NoTenantsForUser(HttpContext context);

        IActionResult SignedOut(HttpContext context);
        IActionResult SignedOut(HttpContext context, IdentityProviderId provider, string redirect);
        IActionResult SignOutExternalProvider(HttpContext context, IdentityProviderId provider);

        IActionResult DeviceSignIn(HttpContext context);
        IActionResult DeviceSignInSuccess(HttpContext context, DeviceFlowAuthorizationRequest deviceContext);
        IActionResult InvalidDeviceUserCode(HttpContext context, string userCode);
        IActionResult DeviceSignInError(HttpContext context, string errorDescription);
        IActionResult DeviceSignInAccessDenied(HttpContext context, DeviceFlowAuthorizationRequest deviceContext);

        IActionResult SpecifiedProviderDoesNotExist(HttpContext context, IdentityProviderId provider);
        IActionResult Error(HttpContext context);
    }
}