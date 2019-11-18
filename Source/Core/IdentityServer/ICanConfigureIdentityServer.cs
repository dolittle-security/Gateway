/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using IdentityServer4.Configuration;

namespace Core.IdentityServer
{
    public interface ICanConfigureIdentityServer
    {
        void Configure(IdentityServerOptions options);
    }
}