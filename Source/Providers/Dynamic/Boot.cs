/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Runtime.Tenancy;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Read.Providers.Configuring;

namespace Providers.Dynamic
{
    public class Boot : ICanPerformBootProcedure
    {
        readonly IExecutionContextManager _contextManager;
        readonly ITenants _tenants;
        readonly FactoryFor<ConfigurationManager> _configurationManagerFactory;
        readonly ILogger _logger;

        public Boot(IExecutionContextManager contextManager, ITenants tenants, FactoryFor<ConfigurationManager> configurationManagerFactory, ILogger logger)
        {
            _contextManager = contextManager;
            _tenants = tenants;
            _configurationManagerFactory = configurationManagerFactory;
            _logger = logger;
        }

        public bool CanPerform() => true;

        public void Perform()
        {
            _tenants.All.ForEach(tenant => {
                Task.Factory.StartNew(() => {
                    try
                    {
                        _contextManager.CurrentFor(tenant);
                        _configurationManagerFactory().Start();
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e, $"Error while startig identity provider configuration manager for tenant '{tenant}'. No providers will be available for that tenant.");
                    }
                });
            });
        }
    }
}