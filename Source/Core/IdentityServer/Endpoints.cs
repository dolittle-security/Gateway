/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using Dolittle.Collections;
using IdentityServer4.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IdentityServer
{
    public static class Endpoints
    {
        public static IIdentityServerBuilder AddCustomEndpoints(this IIdentityServerBuilder builder)
        {
            builder.AddDefaultEndpoints();
            foreach (var endpointDescriptor in builder.Services.Where(_ => _.ServiceType.Equals(typeof(Endpoint))).ToList())
            {
                if (endpointDescriptor.ImplementationInstance is Endpoint endpoint)
                {
                    switch (endpoint.Name)
                    {
                        case "Userinfo":
                        builder.Services.OverrideEndpoint<UserInfoEndpoint>(endpoint);
                        break;
                    }
                }
            }
            return builder;
        }

        static void OverrideEndpoint<T>(this IServiceCollection services, Endpoint endpoint) where T : IEndpointHandler
        {
            var endpointHandlerServiceDescriptor = services.FirstOrDefault(_ => _.ServiceType.Equals(endpoint.Handler));
            if (endpointHandlerServiceDescriptor != null)
            {
                services.Remove(endpointHandlerServiceDescriptor);
            }
            endpoint.Handler = typeof(T);
            services.AddTransient(typeof(T));
        }
    }
}