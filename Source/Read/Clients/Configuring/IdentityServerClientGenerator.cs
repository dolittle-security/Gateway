/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using static IdentityModel.OidcConstants;
using IdentityServerClient = IdentityServer4.Models.Client;

namespace Read.Clients.Configuring
{
    public class IdentityServerClientGenerator : IIdentityServerClientGenerator
    {
        IdentityServerClient CreateBaseConfigFor(Client client)
        {
            var config = new IdentityServerClient();

            config.ClientId = client.Id.ToString();
            config.ClientName = client.Name;
            config.Description = client.Description;

            return config;
        }

        public IdentityServerClient From(DeviceClient client)
        {
            var config = CreateBaseConfigFor(client);

            config.AllowedGrantTypes.Add(GrantTypes.DeviceCode);
            config.AllowedScopes.Add("openid");
            config.RequireClientSecret = false;

            return config;
        }
    }
}