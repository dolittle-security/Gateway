/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Signin
{
    [Authorize]
    [Route("device")]
    public class DeviceController : ControllerBase
    {
        readonly IDeviceFlowInteractionService _interaction;

        public DeviceController(IDeviceFlowInteractionService interaction)
        {
            _interaction = interaction;
        }

        public async Task<IActionResult> Verify(string userCode)
        {
            var context = await _interaction.GetAuthorizationContextAsync(userCode);
            var result = await _interaction.HandleRequestAsync(userCode, new ConsentResponse {
                ScopesConsented = context.ScopesRequested,
            });
            return Ok("RESULT: "+result.ErrorDescription);
        }
    }
}