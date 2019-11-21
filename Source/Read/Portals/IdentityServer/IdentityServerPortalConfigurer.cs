/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Portals;
using Context;
using IdentityServer4.Configuration;
using MongoDB.Driver;
using Read.Clients.Configuring;

namespace Read.Portals.IdentityServer
{
    public class IdentityServerPortalConfigurer : ICanConfigureIdentityServerForPortal
    {
        readonly IMongoCollection<DeviceClient> _deviceClients;

        public IdentityServerPortalConfigurer(IMongoCollection<DeviceClient> deviceClients)
        {
            _deviceClients = deviceClients;
        }

        public void ConfigureFor(PortalContext context, IdentityServerOptions options)
        {
            if (PortalHasDeviceClients(context.Portal))
            {
                options.Endpoints.EnableDeviceAuthorizationEndpoint = true;
                options.Endpoints.EnableTokenEndpoint = true;
                options.Endpoints.EnableUserInfoEndpoint = true;
                options.Endpoints.EnableDiscoveryEndpoint = true;
            }
        }

        bool PortalHasDeviceClients(PortalId portal)
        {
            var filter = Builders<DeviceClient>.Filter.Eq(_ => _.Portal, portal);
            return _deviceClients.Find(filter).Any();
        }
    }
}