/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Claims
{
    public class ClaimType : ConceptAs<string>
    {
        public static explicit operator ClaimType(string type)
        {
            if (TryFromString(type, out var claimType))
            {
                return claimType;
            }
            else
            {
                throw new ClaimTypeContainsInvalidCharacters(type);
            }
        }

        public static bool TryFromString(string type, out ClaimType claimType)
        {
            claimType = default(ClaimType);
            if (type.Contains(",")) return false;
            if (type.Contains("=")) return false;
            claimType = new ClaimType { Value = type };
            return true;
        }
    }
}
