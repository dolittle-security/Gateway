/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Claims;
using Concepts.Portals;
using Dolittle.Tenancy;
using MongoDB.Bson;

namespace Read.Portals.UserMapping
{
    public class ProviderSubjectTenantsForMapping
    {
        public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
        public PortalId Portal { get; set; }
        public IdentityProviderClaim Provider { get; set; }
        public SubjectClaim Subject { get; set; }
        public ICollection<TenantId> Tenants { get; set; }
    }
}