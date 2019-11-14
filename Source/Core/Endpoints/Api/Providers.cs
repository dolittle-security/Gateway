/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Concepts.Providers;
using Microsoft.AspNetCore.Mvc;
using Read.Providers.Choosing;

namespace Core.Endpoints.Api
{
    [Route("api/Dolittle/Sentry/Gateway/Providers")]
    public class Providers : ControllerBase
    {
        readonly ICanResolveProvidersForChoosing _resolver;

        public Providers(ICanResolveProvidersForChoosing resolver)
        {
            _resolver = resolver;
        }

        [HttpGet]
        public IEnumerable<IdentityProviderForChoosing> AllProviders() => _resolver.AllAvailableIdentityProvidersForChoosing();

        [HttpGet("{providerId}")]
        public ActionResult<IdentityProviderForChoosing> SingleProvider(Guid providerId)
        {
            if (_resolver.TryGetProvider(providerId, out var provider))
            {
                return provider;
            }
            else
            {
                return NotFound();
            }
        }
    }
}