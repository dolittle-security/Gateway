/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;
using IdentityServer4.Services;
using IdentityServer4.Stores;

namespace Read.Clients.Configuring
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IClientStore>().To<ClientStore>();
            builder.Bind<IResourceStore>().To<ResourceStore>();
        }
    }
}