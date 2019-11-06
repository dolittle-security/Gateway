/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Claims;

namespace Core.Providers
{
    public interface IPrincipalHomogenizer
    {
        ClaimsPrincipal Homogenize(string scheme, ClaimsPrincipal original);
    }
}