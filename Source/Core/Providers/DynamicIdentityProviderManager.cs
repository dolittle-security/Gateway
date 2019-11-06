/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Core.Providers
{
    public class DynamicIdentityProviderManager : IDynamicIdenityProviderManager
    {
        readonly IContainer _container;
        readonly IAuthenticationSchemeProvider _schemeProvider;
        readonly IPrincipalHomogenizer _homogenizer;

        public DynamicIdentityProviderManager(IContainer container, IAuthenticationSchemeProvider schemeProvider, IPrincipalHomogenizer homogenizer)
        {
            _container = container;
            _schemeProvider = schemeProvider;
            _homogenizer = homogenizer;
        }

        public void AddIdentityProvider<THandler, TOptions>(Guid id, TOptions options)
            where THandler : RemoteAuthenticationHandler<TOptions>
            where TOptions : RemoteAuthenticationOptions, new()
        {
            _schemeProvider.AddScheme(new AuthenticationScheme(id.ToString(), id.ToString(), typeof(THandler)));
            options.Events.OnTicketReceived = OnTicketReceived;
            var postConfigure = _container.Get<IPostConfigureOptions<TOptions>>();
            postConfigure.PostConfigure(id.ToString(), options);
            var cache = _container.Get<IOptionsMonitorCache<TOptions>>();
            cache.TryAdd(id.ToString(), options);
        }

        Task OnTicketReceived(TicketReceivedContext context)
        {
            context.Principal = _homogenizer.Homogenize(context.Scheme.Name, context.Principal);
            return Task.CompletedTask;
        }

        public void RemoveIdentityProvider(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}