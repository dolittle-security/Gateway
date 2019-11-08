/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Microsoft.AspNetCore.Authentication;

namespace Core.Authentication
{
    public static class IdentityTokenAuthenticationHandlerExtensions
    {
        public static AuthenticationBuilder AddIdentityToken(this AuthenticationBuilder builder, string authenticationScheme, Action<IdentityTokenAuthenticationOptions> configureOptions = null)
        {
            return builder.AddScheme<IdentityTokenAuthenticationOptions,IdentityTokenAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }
}