/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;

namespace Context
{
    public static class PortalContextMiddlewareAppExtensions
    {
        public static void UsePortalContext(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<PortalContextMiddelware>();
        }
    }
}