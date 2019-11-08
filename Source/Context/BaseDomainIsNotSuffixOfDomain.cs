/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Context
{
    public class BaseDomainIsNotSuffixOfDomain : Exception
    {
        public BaseDomainIsNotSuffixOfDomain(string baseDomain, string domain) : base($"Base domain '{baseDomain}' is not a suffix of the domain '{domain}'.") {}
    }
}