/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Authentication;

namespace Core.Authentication
{
    public class CompositeAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string CompositeSchemeName = "Dolittle.Composite";
        public const string CookieSchemeName = "Dolittle.Cookie";
        public const string IdentityTokenSchemeName = "Dolittle.Bearer";
    }
}