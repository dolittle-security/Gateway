/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Concepts.Claims
{
    public class SubjectClaimInvalidCharacters : Exception
    {
        public SubjectClaimInvalidCharacters(string sub) : base($"SubjectClaim claim cannot contain commas (,). The provided value was '{sub}'.") {}
    }
}
