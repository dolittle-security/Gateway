/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Context;
using IdentityServer4.Configuration;

namespace Read.Portals.IdentityServer
{
    public interface ICanConfigureIdentityServerForPortal
    {
        void ConfigureFor(PortalContext context, IdentityServerOptions options);
    }
}