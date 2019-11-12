/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Concepts.Providers;
using Core.Pages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Read.Providers.Choosing;

namespace Core.Authentication
{
    public class RemoteAuthenticator : ICanTriggerRemoteAuthentication
    {
        readonly IAuthenticationFrontend _frontend;
        readonly ICanResolveProvidersForChoosing _providers; // TODO: Use another readmodel to check whether this provider is valid?

        public RemoteAuthenticator(IAuthenticationFrontend frontend, ICanResolveProvidersForChoosing providers)
        {
            _frontend = frontend;
            _providers = providers;
        }

        public IActionResult Challenge(HttpContext context, IdentityProviderId providerId, Uri redirectUri)
        {
            if (_providers.AllAvailableIdentityProvidersForChoosing().Any(_ => _.Id == providerId))
            {
                // TODO: Set some state properties here?
                return new ChallengeResult(providerId.ToString(), new AuthenticationProperties {
                    RedirectUri = redirectUri.ToString(),
                });
            }
            else
            {
                return _frontend.SpecifiedProviderDoesNotExist(context, providerId);
            }
        }
    }
}