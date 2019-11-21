/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Endpoints.External
{
    [Route("Dolittle/Sentry/Gateway/Device")]
    public class Device : ControllerBase
    {
        private readonly IDeviceFlowInteractionService _interaction;

        public Device(IDeviceFlowInteractionService interaction)
        {
            _interaction = interaction;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromForm] string userCode)
        {
            var context = await _interaction.GetAuthorizationContextAsync(userCode);
            if (context == null)
            {
                return NotFound();
            }
            
            var result = await _interaction.HandleRequestAsync(userCode, new ConsentResponse {
                RememberConsent = true,
                ScopesConsented = context.ScopesRequested,
            });

            return Ok($"Result: {result.IsAccessDenied} - {result.IsError} - {result.ErrorDescription}");
        }
    }
}