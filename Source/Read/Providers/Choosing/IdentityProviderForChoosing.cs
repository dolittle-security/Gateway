/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Providers;

namespace Read.Providers.Choosing
{
    public class IdentityProviderForChoosing
    {
        public IdentityProviderId Id { get; set; }
        public IdentityProviderDisplayName Name { get; set; }
    }
}