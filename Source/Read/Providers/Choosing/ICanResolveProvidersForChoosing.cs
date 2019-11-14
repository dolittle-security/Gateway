/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Providers;

namespace Read.Providers.Choosing
{
    public interface ICanResolveProvidersForChoosing
    {
        IEnumerable<IdentityProviderForChoosing> AllAvailableIdentityProvidersForChoosing();
        bool TryGetProvider(IdentityProviderId id, out IdentityProviderForChoosing provider);
    }
}