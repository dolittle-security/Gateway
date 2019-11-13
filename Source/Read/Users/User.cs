/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Claims;
using Concepts.Users;

namespace Read.Users
{
    public class User
    {
        public UserId Id { get; set; }
        public ICollection<IssuerSubjectPair> Mappings { get; set; }
    }
}