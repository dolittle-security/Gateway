/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;

namespace Providers.Claims
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IPrincipalHomogenizer>().To<PrincipalHomogenizer>();
        }
    }
}