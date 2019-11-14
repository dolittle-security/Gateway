/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Dolittle.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Endpoints.External
{
    [Route("Dolittle/Sentry/Gateway/SignOut")]
    public class SignOut : ControllerBase
    {
        readonly ILogger _logger;

        public SignOut(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet("External")]
        public async Task External(Guid idp, string rd)
        {
            rd = rd ?? "/signout";
            try
            {
                await HttpContext.SignOutAsync(idp.ToString(), new AuthenticationProperties {
                    RedirectUri = rd,
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error signing out of external provider '{idp}'");
            }

            if (HttpContext.Response.StatusCode != StatusCodes.Status302Found)
            {
                HttpContext.Response.Redirect(rd, false);
            }
        }
    }
}