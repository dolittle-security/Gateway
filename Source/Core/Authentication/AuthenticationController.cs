/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using System.Web;
using Dolittle.Collections;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Core.Authentication
{
    //[Authorize]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Authenticate()
        {
            var result = await HttpContext.AuthenticateAsync("Dolittle.Sentry");
            if (result.Succeeded)
            {
                var claims = "Cookie OK!\nClaims:\n";
                result.Principal.Claims.ForEach(_ => claims += $"{_.Type}: {_.Value}\n");
                return Ok(claims);
            }
            else
            {
                var bearerResult = await HttpContext.AuthenticateAsync("Dolittle.Bearer");
                if (bearerResult.Succeeded)
                {
                    var claims = "Bearer OK!\nClaims:\n";
                    bearerResult.Principal.Claims.ForEach(_ => claims += $"{_.Type}: {_.Value}\n");
                    return Ok(claims);
                }
                else
                {
                    //return Unauthorized();
                    var url = HttpUtility.UrlEncode("/auth");
                    return Redirect($"/signin?rd={url}");
                }
            }
        }
    }
}