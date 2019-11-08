/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Context
{
    public class SingletonHeaderHasNoValueSet : Exception
    {
        public SingletonHeaderHasNoValueSet(string headerName) : base($"Header '{headerName}' is required to have a single value set. No value was found in headers.") {}
    }
}