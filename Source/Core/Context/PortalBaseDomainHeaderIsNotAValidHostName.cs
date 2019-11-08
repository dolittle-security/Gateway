/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Core.Context
{
    public class PortalBaseDomainHeaderIsNotAValidHostName : Exception
    {
        public PortalBaseDomainHeaderIsNotAValidHostName(string headerName, string value) : base($"Portal base domain header '{headerName}' is not a valid hostname. Found value: '{value}'.") {}
    }
}