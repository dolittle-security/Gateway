/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Authentication;
using Dolittle.Commands.Handling;
using Domain.Clients.Device;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Domain.Clients
{
    public class CommandHandler : ICanHandleCommands
    {
        readonly IHttpContextAccessor _contextAccessor;
        readonly IDeviceFlowInteractionService _interaction;

        public CommandHandler(IHttpContextAccessor contextAccessor, IDeviceFlowInteractionService interaction)
        {
            _contextAccessor = contextAccessor;
            _interaction = interaction;
        }

        public void Handle(AuthorizeDeviceWithUserCode command)
        {
            // TODO: We should use some rules here...
            var authResult = _contextAccessor.HttpContext.AuthenticateAsync(Constants.InternalCookieSchemeName).Result;
            if (!authResult.Succeeded)
            {
                throw new Exception("Unauthorized");
            }

            var context = _interaction.GetAuthorizationContextAsync(command.UserCode).Result;
            if (context == null)
            {
                throw new Exception("No context found");
            }

            var result = _interaction.HandleRequestAsync(command.UserCode, new ConsentResponse {
                RememberConsent = true,
                ScopesConsented = context.ScopesRequested,
            }).Result;

            if (result.IsError)
            {
                throw new Exception(result.ErrorDescription);
            }
            if (result.IsAccessDenied)
            {
                throw new Exception("Access denied");
            }
        }
    }
}