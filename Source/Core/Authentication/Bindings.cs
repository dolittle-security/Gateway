/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;

namespace Core.Authentication
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ICanTriggerRemoteAuthentication>().To<RemoteAuthenticator>();
            builder.Bind<ICanSignUserInToTenant>().To<LocalAuthenticator>();
            builder.Bind<ICanGenerateTenantPrincipal>().To<TenantPrincipalGenerator>();
        }
    }
}