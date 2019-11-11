/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.IO;
using System.Text;
using Concepts.Providers;
using Dolittle.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Pages
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

        public IActionResult Error(HttpContext context)
        {
            _logger.Information($"Serving the Error page");
            return IndexPage();
        }

        public IActionResult NoProvidersAvailable(HttpContext context)
        {
            _logger.Information($"Serving the NoProvidersAvailable page");
            return new RedirectResult("/signin/error?id=no-provider-available", false);
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