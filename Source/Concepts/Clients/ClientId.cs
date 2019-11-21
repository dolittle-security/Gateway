/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Concepts;

namespace Concepts.Clients
{
    public class ClientId : ConceptAs<Guid>
    {
        public static implicit operator ClientId(Guid id) => new ClientId { Value = id };
    }
}
