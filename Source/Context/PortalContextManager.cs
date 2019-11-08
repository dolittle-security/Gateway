/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading;
using Concepts;

namespace Context
{
    public class PortalContextManager : IPortalContextManager
    {
        static AsyncLocal<PortalContext> _current = new AsyncLocal<PortalContext>();

        public PortalContext Current => _current.Value;
        
        public void SetPortal(PortalId portal)
        {
            _current.Value = new PortalContext(portal, _current.Value.BaseDomain, _current.Value.SubDomain);
        }

        public void SetBaseDomain(string baseDomain)
        {
            _current.Value = new PortalContext(_current.Value.Portal, baseDomain, _current.Value.SubDomain);
        }


        public void SetSubDomain(string subDomain)
        {
            _current.Value = new PortalContext(_current.Value.Portal, _current.Value.BaseDomain, subDomain);
        }
    }
}