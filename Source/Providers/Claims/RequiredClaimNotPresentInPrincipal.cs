/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Providers.Claims
{
    public class RequiredClaimNotPresentInPrincipal : Exception
    {
        public RequiredClaimNotPresentInPrincipal(string claimType) : base($"No values for the required claim type '{claimType}' was present on the provided principal.") {}
    }
}