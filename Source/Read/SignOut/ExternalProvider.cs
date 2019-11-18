/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Queries;
using Read.Providers.Choosing;

namespace Read.SignOut
{
    public class ExternalProvider : IQueryFor<Provider>
    {
        readonly ICanResolveProvidersForChoosing _resolver;

        public ExternalProvider(ICanResolveProvidersForChoosing resolver)
        {
            _resolver = resolver;
        }

        public Guid ProviderId { get; set; }

        public IQueryable<Provider> Query
        {
            get
            {
                if (_resolver.TryGetProvider(ProviderId, out var provider))
                {
                    return new Provider[] {
                        new Provider
                        {
                            Id = provider.Id,
                            Name = provider.Name
                        }
                    }.AsQueryable();
                }

                return new Provider[0].AsQueryable();
            }
        }
    }
}