/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;
using IdentityServer4.Services;

namespace Core.IdentityServer
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IIdentityServerOptionsProvider>().To<IdentityServerOptionsProvider>();
            builder.Bind<ITokenService>().To<TokenGenerator>();
        }
    }
}