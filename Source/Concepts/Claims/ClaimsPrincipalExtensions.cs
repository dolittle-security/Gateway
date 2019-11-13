/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;

namespace Concepts.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetIssuerSubjectClaims(this ClaimsPrincipal principal, out IssuerClaim issuer, out SubjectClaim subject)
        {
            var issuerFound = false;
            var subjectFound = false;
            issuer = "";
            subject = "";
            foreach (var claim in principal.Claims)
            {
                if (claim.IsIssuerClaim(out var issuerClaim))
                {
                    issuerFound = true;
                    issuer = issuerClaim;
                }
                if (claim.IsSubjectClaim(out var subjectClaim))
                {
                    subjectFound = true;
                    subject = subjectClaim;
                }
            }
            return issuerFound && subjectFound;
        }
    }
}
