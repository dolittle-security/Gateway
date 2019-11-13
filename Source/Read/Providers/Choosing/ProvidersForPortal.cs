/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using Dolittle.Queries;

namespace Read.Providers.Choosing
{
    public class ProvidersForPortal : IQueryFor<IdentityProviderForChoosing>
    {
        readonly ICanResolveProvidersForChoosing _resolver;

        public ProvidersForPortal(ICanResolveProvidersForChoosing resolver)
        {
            _resolver = resolver;
        }

        public IQueryable<IdentityProviderForChoosing> Query => _resolver.AllAvailableIdentityProvidersForChoosing().AsQueryable();
    }
}