/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using System.Web;
using Core.Authentication;
using Dolittle.Collections;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Core.Endpoints
{
    [Route("auth")]
    public class Authentication : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Authenticate()
        {
            var result = await HttpContext.AuthenticateAsync(CompositeAuthenticationOptions.CompositeSchemeName);
            if (result.Succeeded)
            {
                var claims = "Claims:\n";
                result.Principal.Claims.ForEach(_ => claims += $"{_.Type}: {_.Value}\n");
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