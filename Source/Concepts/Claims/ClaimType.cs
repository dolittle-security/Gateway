/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Claims
{
    public class ClaimType : ConceptAs<string>
    {
        public static implicit operator ClaimType(string type) => new ClaimType { Value = type };
    }
}
