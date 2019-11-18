/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Context;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Tenancy;
using Dolittle.Types;
using IdentityServer4.Configuration;
using Read.Portals.IdentityServer;

namespace Core.IdentityServer
{
    [Singleton]
    public class IdentityServerOptionsProvider : IIdentityServerOptionsProvider
    {
        readonly IExecutionContextManager _executionManager;
        readonly IPortalContextManager _portalManager;
        readonly IInstancesOf<ICanConfigureIdentityServer> _configurers;
        readonly FactoryFor<ICanConfigureIdentityServerForPortal> _portalConfigurerFactory;
        readonly IdentityServerOptions _defaultOptions;

        public IdentityServerOptionsProvider(
            IExecutionContextManager executionManager,
            IPortalContextManager portalManager,
            IInstancesOf<ICanConfigureIdentityServer> configurers,
            FactoryFor<ICanConfigureIdentityServerForPortal> portalConfigurerFactory
        )
        {
            _executionManager = executionManager;
            _portalManager = portalManager;
            _configurers = configurers;
            _portalConfigurerFactory = portalConfigurerFactory;
            _defaultOptions = DefaultOptions();
        }

        IdentityServerOptions DefaultOptions()
        {
            var options = new IdentityServerOptions();
            foreach (var configurer in _configurers)
            {
                configurer.Configure(options);
            }
            return options;
        }

        public IdentityServerOptions GetOptions()
        {
            var tenantId = _executionManager.Current.Tenant;
            if (tenantId == TenantId.Unknown || tenantId == TenantId.System)
            {
                return _defaultOptions;
            }
            else
            {
                var options = DefaultOptions();
                var portalContext = _portalManager.Current;
                var portalConfigurer = _portalConfigurerFactory();
                portalConfigurer.ConfigureFor(portalContext, options);
                return options;
            }
        }
    }
}