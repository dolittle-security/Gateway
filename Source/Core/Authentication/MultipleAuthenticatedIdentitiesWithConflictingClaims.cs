/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Core.Authentication
{
    public class MultipleAuthenticatedIdentitiesWithConflictingClaims : Exception
    {
        public MultipleAuthenticatedIdentitiesWithConflictingClaims() : base("Multiple authenticated identites with conflicting claims was present on the request.") {}
    }
}