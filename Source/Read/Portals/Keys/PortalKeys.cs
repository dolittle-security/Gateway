/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Portals;

namespace Read.Portals.Keys
{
    public class PortalKeys
    {
        public PortalId Id { get; set; }
        public PortalKey SigingKey { get; set; }
        public ICollection<PortalKey> ValidationKeys { get; set; }
    }
}