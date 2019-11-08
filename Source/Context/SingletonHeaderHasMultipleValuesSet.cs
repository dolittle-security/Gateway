/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Context
{
    public class SingletonHeaderHasMultipleValuesSet : Exception
    {
        public SingletonHeaderHasMultipleValuesSet(string headerName, IEnumerable<string> values) : base($"Header '{headerName}' is required to have a single value set. Multiple value was found in headers: '{string.Join("','", values)}'.") {}
    }
}