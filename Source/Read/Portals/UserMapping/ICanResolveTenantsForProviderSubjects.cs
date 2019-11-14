/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Security.Claims;
using Concepts.Claims;
using Dolittle.Tenancy;

namespace Read.Portals.UserMapping
{
    public interface ICanResolveTenantsForProviderSubjects
    {
        IEnumerable<TenantId> GetTenantsFor(IdentityProviderClaim issuer, SubjectClaim subject);
        IEnumerable<TenantId> GetTenantsFor(ClaimsPrincipal principal);
    }
}