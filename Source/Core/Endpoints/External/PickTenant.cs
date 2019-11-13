/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Core.Endpoints.External
{
    [Route("Dolittle/Sentry/Gateway/PickTenant")]
    public class PickTenant : ControllerBase
    {
        private readonly ICanSignUserInToTenant _authenticator;

        public PickTenant(ICanSignUserInToTenant authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpGet]
        public IActionResult Signin(Uri rd, Guid tenant)
        {
            return _authenticator.SignUserInTo(HttpContext, tenant, rd);
        }
    }
}