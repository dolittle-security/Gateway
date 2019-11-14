/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Concepts.Claims;
using Context;
using Dolittle.Logging;
using Dolittle.Tenancy;
using MongoDB.Driver;

namespace Read.Portals.UserMapping
{
    public class TenantResolver : ICanResolveTenantsForProviderSubjects
    {
        readonly IPortalContextManager _manager;
        readonly IMongoCollection<ProviderSubjectTenantsForMapping> _mappings;
        readonly ILogger _logger;

        public TenantResolver(IPortalContextManager manager, IMongoCollection<ProviderSubjectTenantsForMapping> mappings, ILogger logger)
        {
            _manager = manager;
            _mappings = mappings;
            _logger = logger;
        }

        public IEnumerable<TenantId> GetTenantsFor(IdentityProviderClaim provider, SubjectClaim subject)
        {
            var portal = _manager.Current.Portal;
            var builder = Builders<ProviderSubjectTenantsForMapping>.Filter;
            var filter = builder.Eq(_ => _.Portal, portal) & builder.Eq(_ => _.Provider, provider) & builder.Eq(_ => _.Subject, subject);

            var tenantMappings = _mappings.Find(filter).ToEnumerable();
            switch (tenantMappings.Count())
            {
                case 0:
                return Enumerable.Empty<TenantId>();

                case 1:
                return tenantMappings.First().Tenants;

                default:
                _logger.Warning($"Found multiple mappings for portal:'{portal}', provider:'{provider}', subject:'{subject}'. All tenants will be returned, but this indicates something wrong in the database, and should be fixed.");
                return tenantMappings.SelectMany(_ => _.Tenants);
            }
        }

        public IEnumerable<TenantId> GetTenantsFor(ClaimsPrincipal principal)
        {
            if (principal.TryGetProviderSubjectClaims(out var provider, out var subject))
            {
                return GetTenantsFor(provider, subject);
            }
            else
            {
                return Enumerable.Empty<TenantId>();
            }
        }
    }
}