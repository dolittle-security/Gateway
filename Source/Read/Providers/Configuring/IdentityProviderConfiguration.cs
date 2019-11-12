/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Claims;
using Concepts.Providers;

namespace Read.Providers.Configuring
{
    public abstract class IdentityProviderConfiguration
    {
        public IdentityProviderId Id { get; set; }
        public IDictionary<ClaimType,ClaimType> ClaimMapping { get; set; }
        public IDictionary<ClaimType,ClaimValue> StaticClaims { get; set; }    
    }
}