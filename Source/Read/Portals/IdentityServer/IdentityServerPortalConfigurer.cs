/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Context;
using IdentityServer4.Configuration;

namespace Read.Portals.IdentityServer
{
    public class IdentityServerPortalConfigurer : ICanConfigureIdentityServerForPortal
    {
        public void ConfigureFor(PortalContext context, IdentityServerOptions options)
        {
            // TODO: Set something here
            System.Console.WriteLine($"!!!!!!! CONFIGURING FOR {context.Portal} {context.BaseDomain}");
            options.Endpoints.EnableDiscoveryEndpoint = true;
        }
    }
}