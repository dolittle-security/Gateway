/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Microsoft.AspNetCore.Authentication;

namespace Authentication.Handlers
{
    public static class BearerTokenAuthenticationHandlerExtensions
    {
        public static AuthenticationBuilder AddBearerToken(this AuthenticationBuilder builder, string authenticationScheme, Action<BearerTokenAuthenticationOptions> configureOptions = null)
        {
            return builder.AddScheme<BearerTokenAuthenticationOptions,BearerTokenAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }
}