/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Concepts.Providers;

namespace Read.Providers.Configuring
{
    public class IdentityProviderConfigurationDoesNotExist : Exception
    {
        public IdentityProviderConfigurationDoesNotExist(IdentityProviderId id) : base($"Configuration for identity provider with id {id} was not found.") {}
    }
}