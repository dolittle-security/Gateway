/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Portals;

namespace Read.Providers.Choosing
{
    public class Portal
    {
        public PortalId Id { get; set; }
        public PortalDisplayName DisplayName { get; set; }
        public ICollection<IdentityProviderForChoosing> Providers { get; set; }
    }
}