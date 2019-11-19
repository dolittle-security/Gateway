/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Claims
{
    public class IdentityProviderClaim : ClaimValue
    {
        public static explicit operator IdentityProviderClaim(string idp)
        {
            if (TryFromString(idp, out var claimValue))
            {
                return new IdentityProviderClaim { Value = claimValue.Value };
            }
            else
            {
                throw new IdentityProviderClaimInvalidCharacters(idp);
            }
        }
    }
}
