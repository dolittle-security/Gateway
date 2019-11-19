/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Concepts.Claims
{
    public class ClaimTypeContainsInvalidCharacters : Exception
    {
        public ClaimTypeContainsInvalidCharacters(string type) : base($"ClaimType cannot contain commas (,) or equals (=). The provided type was '{type}'.") {}
    }
}
