/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Concepts;

namespace Concepts.Portals
{
    public class PortalId : ConceptAs<Guid>
    {
        public static implicit operator PortalId(Guid id) => new PortalId { Value = id };
    }
}
