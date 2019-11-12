/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Providers.Claims
{
    public class MultipleClaimsForSingleRequiredClaimPresentInPrincipal : Exception
    {
        public MultipleClaimsForSingleRequiredClaimPresentInPrincipal(string claimType) : base($"Muliple claim values of claim type '{claimType}' was present the provided principal, but a single value was expected.") {}
    }
}