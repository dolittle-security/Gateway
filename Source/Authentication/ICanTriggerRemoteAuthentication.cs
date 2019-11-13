/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Concepts.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication
{
    public interface ICanTriggerRemoteAuthentication
    {
        IActionResult Challenge(HttpContext context, IdentityProviderId providerId, Uri redirectUri);
    }
}