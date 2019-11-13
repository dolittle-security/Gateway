/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;
using Concepts.Claims;

namespace Read.Users
{
    public interface ICanResolveUserForProviderSubjects
    {
        bool TryGetUserFor(IssuerClaim issuer, SubjectClaim subject, out User user);
        bool TryGetUserFor(ClaimsPrincipal principal, out User user);
    }
}