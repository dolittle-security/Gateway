/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Claims
{
    public class SubjectClaim : ClaimValue
    {
        public static explicit operator SubjectClaim(string sub)
        {
            if (TryFromString(sub, out var claimValue))
            {
                return new SubjectClaim{ Value = sub };
            }
            else
            {
                throw new SubjectClaimInvalidCharacters(sub);
            }
        }
    }
}
