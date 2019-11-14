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
        const string IdentityProviderClaimType = "iss";
        const string SubjectClaimType = "sub";

        public static bool IsIdentityProviderClaim(this Claim claim, out IdentityProviderClaim identityProviderClaim)
        {
            identityProviderClaim = claim.Value;
            return claim.Type.Equals(IdentityProviderClaimType, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsSubjectClaim(this Claim claim, out SubjectClaim subjectClaim)
        {
            subjectClaim = claim.Value;
            return claim.Type.Equals(SubjectClaimType, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
