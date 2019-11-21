/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using Authentication.Frontend;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication
{
    public class DeviceAuthorizer : ICanHandleDeviceAuthorization
    {
        readonly IDeviceFlowInteractionService _interaction;
        readonly IAuthenticationFrontend _frontend;

        public DeviceAuthorizer(IDeviceFlowInteractionService interaction, IAuthenticationFrontend frontend)
        {
            _interaction = interaction;
            _frontend = frontend;
        }

        public async Task<IActionResult> HandleAsync(HttpContext context, string userCode)
        {
            var deviceContext = await _interaction.GetAuthorizationContextAsync(userCode);
            if (deviceContext == null)
            {
                return _frontend.InvalidDeviceUserCode(context, userCode);
            }
            
            // TODO: User conscent view
            
            var result = await _interaction.HandleRequestAsync(userCode, new ConsentResponse {
                RememberConsent = true,
                ScopesConsented = deviceContext.ScopesRequested,
            });

            if (result.IsError)
            {
                return _frontend.DeviceSignInError(context, result.ErrorDescription);
            }
            else if (result.IsAccessDenied)
            {
                return _frontend.DeviceSignInAccessDenied(context, deviceContext);
            }
            else
            {
                return _frontend.DeviceSignInSuccess(context, deviceContext);
            }
        }
    }
}