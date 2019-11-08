/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Providers
{
    public class IdentityProviderDisplayName : ConceptAs<string>
    {
        public static implicit operator IdentityProviderDisplayName(string name) => new IdentityProviderDisplayName { Value = name };
    }
}
