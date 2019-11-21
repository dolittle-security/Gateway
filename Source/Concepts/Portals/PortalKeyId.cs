/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Concepts;

namespace Concepts.Portals
{
    public class PortalKeyId : ConceptAs<Guid>
    {
        public static implicit operator PortalKeyId(Guid id) => new PortalKeyId { Value = id };
    }
}
