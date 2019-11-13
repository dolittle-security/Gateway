/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Microsoft.AspNetCore.Authentication;

namespace Authentication.Handlers
{
    public static class CompositeAuthenticationHandlerExtensions
    {
        public static AuthenticationBuilder AddComposite(this AuthenticationBuilder builder, string authenticationScheme, Action<CompositeAuthenticationOptions> configureOptions = null)
        {
            return builder.AddScheme<CompositeAuthenticationOptions, CompositeAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }
}