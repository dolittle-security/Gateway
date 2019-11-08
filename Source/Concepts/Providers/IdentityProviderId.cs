/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Concepts;

namespace Concepts.Providers
{
    public class IdentityProviderId : ConceptAs<Guid>
    {
        public static implicit operator IdentityProviderId(Guid id) => new IdentityProviderId { Value = id };
    }
}
