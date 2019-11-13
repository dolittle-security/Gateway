/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;
using Read.Users;

namespace Core.Authentication
{
    public interface ICanGenerateTenantPrincipal
    {
        ClaimsPrincipal GenerateFor(User user);
    }
}