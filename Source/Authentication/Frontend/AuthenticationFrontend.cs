/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.IO;
using System.Text;
using Concepts.Providers;
using Dolittle.Logging;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Frontend
{
    public class AuthenticationFrontend : IAuthenticationFrontend
    {
        readonly ILogger _logger;

        public AuthenticationFrontend(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult ChooseProvider(HttpContext context)
        {
            _logger.Information($"Serving the ChooseProvider page");
            return IndexPage();
        }

        public IActionResult ChooseTenant(HttpContext context)
        {
            _logger.Information($"Serving the ChooseTenant page");
            return IndexPage();
        }

        public IActionResult DeviceSignIn(HttpContext context)
        {
            _logger.Information($"Serving the DeviceSignIn page");
            return IndexPage();
        }

        public IActionResult DeviceSignInAccessDenied(HttpContext context, DeviceFlowAuthorizationRequest deviceContext)
        {
            _logger.Information($"Serving the DeviceSignInAccessDenied page");
            return new RedirectResult("/signin/error?id=device-access-denied", false);
        }

        public IActionResult DeviceSignInError(HttpContext context, string errorDescription)
        {
            _logger.Information($"Serving the DeviceSignInError page");
            return new RedirectResult($"/signin/error?id=device-error&message={errorDescription}", false);
        }

        public IActionResult DeviceSignInSuccess(HttpContext context, DeviceFlowAuthorizationRequest deviceContext)
        {
            _logger.Information($"Serving the DeviceSignInSuccess page");
            return new OkResult();
        }

        public IActionResult Error(HttpContext context)
        {
            _logger.Information($"Serving the Error page");
            return IndexPage();
        }

        public IActionResult InvalidDeviceUserCode(HttpContext context, string userCode)
        {
            _logger.Information($"Serving the InvalidDeviceUserCode page");
            return new RedirectResult("/signin/error?id=device-user-code-invalid", false);
        }

        public IActionResult NoProvidersAvailable(HttpContext context)
        {
            _logger.Information($"Serving the NoProvidersAvailable page");
            return new RedirectResult("/signin/error?id=no-provider-available", false);
        }

        public IActionResult NoTenantsForUser(HttpContext context)
        {
            _logger.Information($"Serving the NoTenantsForUser page");
            return new RedirectResult("/signin/error?id=no-tenants-for-user", false);
        }

        public IActionResult SignedOut(HttpContext context)
        {
            _logger.Information($"Serving the SignedOut page");
            return IndexPage();
        }

        public IActionResult SignedOut(HttpContext context, IdentityProviderId provider, string redirect)
        {
            _logger.Information($"Serving the SignedOut page - with external provider");
            return new RedirectResult($"/signout/external?idp={provider}&rd={redirect}", false);
        }

        public IActionResult SignOutExternalProvider(HttpContext context, IdentityProviderId provider)
        {
            _logger.Information($"Serving the SignOutExternalProvider page");
            return IndexPage();
        }

        public IActionResult SpecifiedProviderDoesNotExist(HttpContext context, IdentityProviderId provider)
        {
            _logger.Information($"Serving the SpecifiedProviderDoesNotExist page");
            return new RedirectResult("/signin/error?id=specified-provider-not-exist", false);
        }

        IActionResult IndexPage()
        {
            return new ContentResult
            {
                StatusCode = StatusCodes.Status200OK,
                ContentType = "text/html; charset=UTF-8",
                Content = File.ReadAllText("./spa.html", Encoding.UTF8),
            };
        }
    }
}