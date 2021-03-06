/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Providers;
using Dolittle.ReadModels;

namespace Read.Providers.Choosing
{
    public class IdentityProviderForChoosing : IReadModel
    {
        public IdentityProviderId Id { get; set; }
        public IdentityProviderDisplayName Name { get; set; }
    }
}