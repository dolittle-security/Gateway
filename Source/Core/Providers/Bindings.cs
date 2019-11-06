/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace Core.Providers
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IPostConfigureOptions<OpenIdConnectOptions>>().To<OpenIdConnectPostConfigureOptions>().Singleton();

            builder.Bind<IDynamicIdenityProviderManager>().To<DynamicIdentityProviderManager>();
        }
    }
}