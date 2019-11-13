/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;
using Read.Portals.UserMapping;
using Read.Providers.Choosing;
using Read.Providers.Configuring;

namespace Read.Providers
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ICanResolveProvidersForChoosing>().To<ProvidersForChoosingResolver>();
            builder.Bind<ICanProvideIdentityProviderConfigurations>().To<IdentityProviderConfigurations>();
            builder.Bind<ICanResolveTenantsForProviderSubjects>().To<TenantResolver>();
        }
    }
}