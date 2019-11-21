/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/


using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Middleware
{
    public class AuthenticationHandlersMiddleware
    {
        readonly RequestDelegate _next;
        readonly IAuthenticationSchemeProvider _schemes;
        readonly IAuthenticationHandlerProvider _handlers;

        public AuthenticationHandlersMiddleware(RequestDelegate next, IAuthenticationSchemeProvider schemes, IAuthenticationHandlerProvider handlers)
        {
            _next = next;
            _schemes = schemes;
            _handlers = handlers;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var handlers = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            foreach (var scheme in await _schemes.GetRequestHandlerSchemesAsync())
            {
                var handler = await handlers.GetHandlerAsync(context, scheme.Name) as IAuthenticationRequestHandler;
                if (handler != null && await handler.HandleRequestAsync())
                {
                    return;
                }
            }
            await _next(context);
        }
    }
}