/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Authentication;
using Authentication.Frontend;
using Microsoft.AspNetCore.Mvc;

namespace Core.Endpoints.External
{
    [Route("Dolittle/Sentry/Gateway/SignIn")]
    public class SignIn : ControllerBase
    {
        readonly ICanTriggerRemoteAuthentication _authenticator;

        public SignIn(ICanTriggerRemoteAuthentication authenticator, IAuthenticationFrontend frontend)
        {
            _authenticator = authenticator;
        }

        [HttpGet]
        public IActionResult Signin(Uri rd, Guid ip)
        {
            return _authenticator.Challenge(HttpContext, ip, rd);
        }
    }
}