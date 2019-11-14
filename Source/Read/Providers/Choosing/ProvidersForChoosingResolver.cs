/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using Concepts.Providers;
using Context;
using MongoDB.Driver;

namespace Read.Providers.Choosing
{
    public class ProvidersForChoosingResolver : ICanResolveProvidersForChoosing
    {
        readonly IPortalContextManager _manager;
        readonly IMongoCollection<Portal> _portals;

        public ProvidersForChoosingResolver(IPortalContextManager manager, IMongoCollection<Portal> portals)
        {
            _manager = manager;
            _portals = portals;
        }

        public IEnumerable<IdentityProviderForChoosing> AllAvailableIdentityProvidersForChoosing()
        {
            if (_portals.TryFindById(_manager.Current.Portal, out var portal))
            {
                return portal.Providers;
            }
            else
            {
                return Enumerable.Empty<IdentityProviderForChoosing>();
            }
        }

        public bool TryGetProvider(IdentityProviderId id, out IdentityProviderForChoosing provider)
        {
            var providers = AllAvailableIdentityProvidersForChoosing().Where(_ => _.Id == id);
            provider = providers.FirstOrDefault();
            return providers.Any();
        }
    }
}