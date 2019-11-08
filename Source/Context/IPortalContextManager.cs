/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts;

namespace Context
{
    public interface IPortalContextManager
    {
        PortalContext Current { get; }
        void SetPortal(PortalId portal);
        void SetBaseDomain(string baseDomain);
        void SetSubDomain(string subDomain);
    }
}