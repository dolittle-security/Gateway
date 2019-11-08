/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Context;

namespace Read.Providers.Choosing
{
    public class ProvidersForChoosingResolver : ICanResolveProvidersForChoosing
    {
        readonly IPortalContextManager _manager;

        public ProvidersForChoosingResolver(IPortalContextManager manager)
        {
            _manager = manager;

        }

        public IEnumerable<IdentityProviderForChoosing> AllAvailableIdentityProvidersForChoosing()
        {
            return new [] {
                new IdentityProviderForChoosing { Id = Guid.NewGuid(), Name = "Azure Ad", },
                new IdentityProviderForChoosing { Id = Guid.NewGuid(), Name = "GitHub", },
            };
        }
    }
}