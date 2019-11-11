/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace Providers.Dynamic
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IIdenityProviderManager>().To<IdentityProviderManager>();
            builder.Bind<IPostConfigureOptions<OpenIdConnectOptions>>().To<OpenIdConnectPostConfigureOptions>().Singleton();
        }
    }
}