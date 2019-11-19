/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Claims
{
    public class ClaimValue : ConceptAs<string>
    {
        public static explicit operator ClaimValue(string value)
        {
            if (TryFromString(value, out var claimValue))
            {
                return claimValue;
            }
            else
            {
                throw new ClaimValueContainsInvalidCharacters(value);
            }
        }

        public static bool TryFromString(string value, out ClaimValue claimValue)
        {
            claimValue = default(ClaimValue);
            if (value.Contains(",")) return false;
            claimValue = new ClaimValue { Value = value };
            return true;
        }
    }
}
