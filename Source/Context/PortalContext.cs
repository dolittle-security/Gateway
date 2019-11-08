/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts;

namespace Context
{
    public struct PortalContext
    {
        public PortalContext(PortalId portal, string baseDomain, string subDomain)
        {
            Portal = portal;
            BaseDomain = baseDomain;
            SubDomain = subDomain;
        }

        public PortalId Portal { get; private set; }
        public string BaseDomain { get; private set; }
        public string SubDomain { get; private set; }

    }
}