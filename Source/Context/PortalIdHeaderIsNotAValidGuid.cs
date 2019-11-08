/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Context
{
    public class PortalIdHeaderIsNotAValidGuid : Exception
    {
        public PortalIdHeaderIsNotAValidGuid(string headerName, string value) : base($"Portal id header '{headerName}' is not a valid Guid. Found value: '{value}'.") {}
    }
}