/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
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

        public Boot(IExecutionContextManager contextManager, ITenants tenants, FactoryFor<ConfigurationManager> configurationManagerFactory)
        {
            _contextManager = contextManager;
            _tenants = tenants;
            _configurationManagerFactory = configurationManagerFactory;
        }

        public bool CanPerform() => true;

        public void Perform()
        {
            _tenants.All.ForEach(tenant => {
                Task.Factory.StartNew(() => {
                    _contextManager.CurrentFor(tenant);
                    _configurationManagerFactory().Start();
                });
            });
        }
    }
}