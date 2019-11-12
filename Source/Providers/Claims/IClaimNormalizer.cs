/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;
using Concepts.Providers;

namespace Providers.Claims
{
    public interface IClaimNormalizer
    {
        ClaimsPrincipal Normalize(IdentityProviderId providerId, ClaimsPrincipal original);
    }
}