/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Authentication;
using Dolittle.Collections;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Core.Endpoints.Internal
{
    [Route("auth")]
    public class Authentication : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Authenticate()
        {
            var result = await HttpContext.AuthenticateAsync(Constants.CompositeSchemeName);
            if (result.Succeeded)
            {
                var claimValueStrings = result.Principal.Claims.Select(_ => $"{_.Type}={_.Value}");
                HttpContext.Response.Headers["Claims"] = string.Join(',', claimValueStrings);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}