/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Security.Claims;

namespace Concepts.Claims
{
    public static class ClaimExtensions
    {
        const string IdentityProviderClaimType = "idp";
        const string SubjectClaimType = "sub";

        public static bool IsIdentityProviderClaim(this Claim claim, out IdentityProviderClaim identityProviderClaim)
        {
            identityProviderClaim = new IdentityProviderClaim();
            if (claim.Type.Equals(IdentityProviderClaimType, StringComparison.InvariantCultureIgnoreCase) && IdentityProviderClaim.TryFromString(claim.Value, out var claimValue))
            {
                identityProviderClaim = new IdentityProviderClaim{ Value = claim.Value };
                return true;
            }
            return false;
        }

        public static bool IsSubjectClaim(this Claim claim, out SubjectClaim subjectClaim)
        {
            subjectClaim = new SubjectClaim();
            if (claim.Type.Equals(SubjectClaimType, StringComparison.InvariantCultureIgnoreCase) && SubjectClaim.TryFromString(claim.Value, out var claimValue))
            {
                subjectClaim = new SubjectClaim{ Value = claim.Value };
                return true;
            }
            return false;
        }
    }
}
