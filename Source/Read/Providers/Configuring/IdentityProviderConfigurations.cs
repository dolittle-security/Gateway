/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Lifecycle;
using MongoDB.Driver;

namespace Read.Providers.Configuring
{
    [SingletonPerTenant]
    public class IdentityProviderConfigurations : ICanProvideIdentityProviderConfigurations
    {
        readonly IMongoCollection<OpenIDConnectConfiguration> _openIdConfigurations;

        public IdentityProviderConfigurations(IMongoCollection<OpenIDConnectConfiguration> openIdConfigurations)
        {
            _openIdConfigurations = openIdConfigurations;
        }

        // TODO: Make class an event-processor and notify about new provider configurations
        public event Action<OpenIDConnectConfiguration> OnOpenIdConnectConfiguraitonAdded = delegate {};

        public IEnumerable<OpenIDConnectConfiguration> AllOpenIdConnectConfigurations()
        {
            return _openIdConfigurations.Find(_ => true).ToEnumerable();
        }
    }
}