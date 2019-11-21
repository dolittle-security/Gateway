/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using IdentityServerClient = IdentityServer4.Models.Client;

namespace Read.Clients.Configuring
{
    public interface IIdentityServerClientGenerator
    {
        IdentityServerClient From(DeviceClient client);
    }
}