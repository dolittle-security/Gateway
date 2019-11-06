/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Core.Services
{
    public class ClientStore : IClientStore
    {
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(new Client {
                ClientId = clientId,
                ClientName = "Test Client",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.DeviceFlow,
                RedirectUris = { "http://localhost:5010/signin-oidc" },
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                },
                RequireConsent = false,
            });
        }
    }
}