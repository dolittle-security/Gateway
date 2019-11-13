/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Web;
using Authentication.Frontend;
using Concepts.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Read.Providers.Choosing;

namespace Authentication
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
                var url = HttpUtility.UrlEncode(redirectUri.ToString());

                // TODO: Set some state properties here?
                return new ChallengeResult(providerId.ToString(), new AuthenticationProperties {
                    RedirectUri = $"/signin/tenant?rd={url}",
                });
            }
            else
            {
                return _frontend.SpecifiedProviderDoesNotExist(context, providerId);
            }
        }
    }
}