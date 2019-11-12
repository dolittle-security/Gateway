/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using Dolittle.Collections;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Providers.Claims;
using Read.Providers.Configuring;

namespace Providers.Dynamic
{
    public class ConfigurationManager
    {
        readonly ICanProvideIdentityProviderConfigurations _configurations;
        readonly IIdenityProviderManager _manager;

        public ConfigurationManager(ICanProvideIdentityProviderConfigurations configurations, IIdenityProviderManager manager)
        {
            _configurations = configurations;
            _manager = manager;
        }

        public void Start()
        {
            _configurations.OnOpenIdConnectConfiguraitonAdded += AddOpenIdConnectProvider;
            _configurations.AllOpenIdConnectConfigurations().ForEach(AddOpenIdConnectProvider);
        }

        void AddOpenIdConnectProvider(OpenIDConnectConfiguration configuration)
        {
            var options = new OpenIdConnectOptions {
                SignInScheme = "Dolittle.Cookie",
                ResponseType = "code",
                Authority = configuration.Authority,
                ClientId = configuration.ClientId,
                ClientSecret = configuration.ClientSecret,
                CallbackPath = $"/signin/oidc-{configuration.Id}",
                SignedOutCallbackPath = $"/signout/oidc-{configuration.Id}",
            };
            options.ClaimActions.Clear();
            _manager.AddIdentityProvider<OpenIdConnectHandler, OpenIdConnectOptions>(configuration.Id, options);
        }
    }
}