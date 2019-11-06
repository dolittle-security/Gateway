/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Core.Error
{
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        readonly IIdentityServerInteractionService _interaction;

        public ErrorController(IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider provider)
        {
            _interaction = interaction;

            foreach (var scheme in provider.GetAllSchemesAsync().Result)
            {
                System.Console.WriteLine($"!!! Scheme {scheme.Name}");
            }

            var def = provider.GetDefaultAuthenticateSchemeAsync().Result;
            System.Console.WriteLine($"!!! Default {def.Name}");
        }

        [HttpGet]
        public async Task<IActionResult> Error(string errorId)
        {
            var message = await _interaction.GetErrorContextAsync(errorId);
            return Ok(message.Error + "\n\n" + message.ErrorDescription);
        }
    }
}