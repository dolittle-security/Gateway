/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Dolittle.Collections;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Core.Signin
{
    [Route("signin")]
    public class SigninController : ControllerBase
    {
        [HttpGet]
        public IActionResult Signin(Uri rd)
        {
            return Challenge(new AuthenticationProperties {
                RedirectUri = rd.ToString(),
            }, "df066550-35dd-4eb3-9610-b1c20d221fc7");
        }
    }
}