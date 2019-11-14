/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Claims;
using Concepts.Providers;

namespace Read.Users
{
    public class ProviderSubjectPair
    {
        public IdentityProviderClaim Provider { get; set; }
        public SubjectClaim Subject { get; set; }
    }
}