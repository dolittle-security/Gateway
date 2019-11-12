/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;

namespace Providers.Claims
{
    public static class ClaimsIdentityExtensions
    {
        public static void AddClaim(this ClaimsIdentity identity, string claimType, string claimValue)
        {
            identity.AddClaim(claimType, claimValue, ClaimValueTypes.String);
        }

        public static void AddClaim(this ClaimsIdentity identity, string claimType, string claimValue, string valueType)
        {
            // TODO: Issuer?
            identity.AddClaim(new Claim(claimType, claimValue, valueType));
        }
    }
}