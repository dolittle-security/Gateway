/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using IdentityServer4.Configuration;

namespace Core.IdentityServer
{
    public class EndpointsDisabledByDefault : ICanConfigureIdentityServer
    {
        public void Configure(IdentityServerOptions options)
        {
            options.Endpoints.EnableAuthorizeEndpoint = false;
            options.Endpoints.EnableCheckSessionEndpoint = false;
            options.Endpoints.EnableDeviceAuthorizationEndpoint = false;
            options.Endpoints.EnableDiscoveryEndpoint = false;
            options.Endpoints.EnableEndSessionEndpoint = false;
            options.Endpoints.EnableIntrospectionEndpoint = false;
            options.Endpoints.EnableJwtRequestUri = false;
            options.Endpoints.EnableTokenEndpoint = false;
            options.Endpoints.EnableTokenRevocationEndpoint = false;
            options.Endpoints.EnableUserInfoEndpoint = false;
        }
    }
}