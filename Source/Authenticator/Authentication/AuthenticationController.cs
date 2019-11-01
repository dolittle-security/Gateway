/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using Dolittle.Collections;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Authentication
{
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Authenticate()
        {
            var result = await HttpContext.AuthenticateAsync("Dolittle");
            if (result.Succeeded)
            {
                var claims = "Claims:\n";
                result.Principal.Claims.ForEach(_ => claims += $"{_.Type}: {_.Value}\n");
                return Ok(claims);
            }
            else
            {
                //return Unauthorized();
                return Redirect("/signin");
            }
        }
    }
}