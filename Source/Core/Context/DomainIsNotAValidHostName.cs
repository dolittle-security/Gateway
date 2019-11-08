/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Core.Context
{
    public class DomainIsNotAValidHostName : Exception
    {
        public DomainIsNotAValidHostName(string domain) : base($"Domain '{domain}' is not a valid hostname.") {}
    }
}