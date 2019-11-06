/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Core.Providers
{
    [Route("providers")]
    public class TestController : ControllerBase
    {
        readonly IDynamicIdenityProviderManager _manager;

        public TestController(IDynamicIdenityProviderManager manager)
        {
            _manager = manager;
        }

        [HttpGet("add")]
        public IActionResult Add(string clientId, string authority)
        {
            _manager.AddIdentityProvider<OpenIdConnectHandler, OpenIdConnectOptions>(Guid.Parse("df066550-35dd-4eb3-9610-b1c20d221fc7"), new OpenIdConnectOptions {
                SignInScheme = "Dolittle.Sentry",
                ClientId = clientId,
                Authority = authority,
                CallbackPath = "/signin/oidc-df066550-35dd-4eb3-9610-b1c20d221fc7",
                SignedOutCallbackPath = "/signout/oidc-df066550-35dd-4eb3-9610-b1c20d221fc7",
            });
            return Ok();
        }
    }
}