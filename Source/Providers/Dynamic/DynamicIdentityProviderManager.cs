/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Providers.Claims;

namespace Providers.Dynamic
{
    public class IdentityProviderManager : IIdenityProviderManager
    {
        readonly IContainer _container;
        readonly IAuthenticationSchemeProvider _schemeProvider;
        readonly IClaimNormalizer _normalizer;

        public IdentityProviderManager(IContainer container, IAuthenticationSchemeProvider schemeProvider, IClaimNormalizer normalizer)
        {
            _container = container;
            _schemeProvider = schemeProvider;
            _normalizer = normalizer;
        }

        public void AddIdentityProvider<THandler, TOptions>(Guid id, TOptions options)
            where THandler : RemoteAuthenticationHandler<TOptions>
            where TOptions : RemoteAuthenticationOptions, new()
        {
            _schemeProvider.AddScheme(new AuthenticationScheme(id.ToString(), id.ToString(), typeof(THandler)));
            options.Events.OnTicketReceived = TicketRecieved;
            var postConfigure = _container.Get<IPostConfigureOptions<TOptions>>();
            postConfigure.PostConfigure(id.ToString(), options);
            var cache = _container.Get<IOptionsMonitorCache<TOptions>>();
            cache.TryAdd(id.ToString(), options);
        }

        Task TicketRecieved(TicketReceivedContext context)
        {
            context.Principal = _normalizer.Normalize(Guid.Parse(context.Scheme.Name), context.Principal);
            return Task.CompletedTask;
        }

        public void RemoveIdentityProvider(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}