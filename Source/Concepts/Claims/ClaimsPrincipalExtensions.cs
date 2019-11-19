/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;

namespace Concepts.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetProviderSubjectClaims(this ClaimsPrincipal principal, out IdentityProviderClaim provider, out SubjectClaim subject)
        {
            var providerFound = false;
            var subjectFound = false;
            provider = new IdentityProviderClaim();
            subject = new SubjectClaim();
            foreach (var claim in principal.Claims)
            {
                if (claim.IsIdentityProviderClaim(out var providerClaim))
                {
                    providerFound = true;
                    provider = providerClaim;
                }
                if (claim.IsSubjectClaim(out var subjectClaim))
                {
                    subjectFound = true;
                    subject = subjectClaim;
                }
                if (providerFound && subjectFound) break;
            }
            return providerFound && subjectFound;
        }
    }
}
