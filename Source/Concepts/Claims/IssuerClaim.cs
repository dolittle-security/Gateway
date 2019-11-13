/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Claims
{
    public class IssuerClaim : ConceptAs<string>
    {
        public static implicit operator IssuerClaim(string iss) => new IssuerClaim { Value = iss };
    }
}
