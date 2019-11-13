/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Concepts;

namespace Concepts.Users
{
    public class UserId : ConceptAs<Guid>
    {
        public static implicit operator UserId(Guid id) => new UserId { Value = id };
    }
}
