/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Portals
{
    public class PortalDisplayName : ConceptAs<string>
    {
        public static implicit operator PortalDisplayName(string name) => new PortalDisplayName { Value = name };
    }
}
