/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Claims;

namespace Read.Users
{
    public class IssuerSubjectPair
    {
        public IssuerClaim Issuer { get; set; }
        public SubjectClaim Subject { get; set; }
    }
}