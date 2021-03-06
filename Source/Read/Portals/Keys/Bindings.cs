/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;
using IdentityServer4.Services;
using MongoDB.Bson.Serialization;

namespace Read.Portals.Keys
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IKeyMaterialService>().To<PortalKeyMaterialService>();
            BsonSerializer.RegisterSerializer(new RsaSecurityKeySerializer());
        }
    }
}