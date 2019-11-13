/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Claims;
using Concepts.Portals;
using Dolittle.Tenancy;

namespace Read.Portals.UserMapping
{
    public class ProviderSubjectTenantsForMapping
    {
        public PortalId Portal { get; set; }
        public IssuerClaim Issuer { get; set; }
        public SubjectClaim Subject { get; set; }
        public ICollection<TenantId> Tenants { get; set; }
    }
}