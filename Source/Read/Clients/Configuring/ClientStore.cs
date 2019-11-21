/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Context;
using Dolittle.Logging;
using IdentityServer4.Stores;
using MongoDB.Driver;
using IdentityServerClient = IdentityServer4.Models.Client;

namespace Read.Clients.Configuring
{
    public class ClientStore : IClientStore
    {
        readonly IPortalContextManager _portalManager;
        readonly IMongoCollection<DeviceClient> _deviceClients;
        readonly IIdentityServerClientGenerator _clientGenerator;
        readonly ILogger _logger;

        public ClientStore(IPortalContextManager portalManager, IMongoCollection<DeviceClient> deviceClients, IIdentityServerClientGenerator clientGenerator, ILogger logger)
        {
            _portalManager = portalManager;
            _deviceClients = deviceClients;
            _clientGenerator = clientGenerator;
            _logger = logger;
        }

        public Task<IdentityServerClient> FindClientByIdAsync(string clientId)
        {
            if (Guid.TryParse(clientId, out var clientIdGuid))
            {
                if (_deviceClients.TryFindById(clientIdGuid, out var client))
                {
                    if (client.Portal == _portalManager.Current.Portal)
                    {
                        return Task.FromResult(_clientGenerator.From(client));
                    }
                    else
                    {
                        _logger.Error($"Client with '{clientId}' was requested for portal '{_portalManager.Current.Portal}' - but is configured for '{client.Portal}'.");
                    }
                }
            }
            return Task.FromResult(default(IdentityServerClient));
        }
    }
}