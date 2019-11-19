/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Concepts.Claims
{
    public class ClaimValueContainsInvalidCharacters : Exception
    {
        public ClaimValueContainsInvalidCharacters(string value) : base($"ClaimValue cannot contain commas (,). The provided value was '{value}'.") {}
    }
}
