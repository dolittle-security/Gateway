/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Claims
{
    public class SubjectClaim : ConceptAs<string>
    {
        public static implicit operator SubjectClaim(string sub) => new SubjectClaim { Value = sub };
    }
}
