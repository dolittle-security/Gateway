/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Concepts.Providers;

namespace Read.Providers.Configuring
{
    public interface ICanProvideIdentityProviderConfigurations
    {
        IdentityProviderConfiguration GetCommonProviderConfiguration(IdentityProviderId id);
        IEnumerable<OpenIDConnectConfiguration> AllOpenIdConnectConfigurations();
        event Action<OpenIDConnectConfiguration> OnOpenIdConnectConfiguraitonAdded;
    }
}