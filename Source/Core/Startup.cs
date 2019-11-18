/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Autofac;
using Context;
using Core.Services;
using Dolittle.Booting;
using Dolittle.DependencyInversion.Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Authentication;
using Authentication.Handlers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Configuration;
using Core.IdentityServer;
using Microsoft.AspNetCore.HttpOverrides;

namespace Core
{
    public partial class Startup
    {
        readonly IHostingEnvironment _hostingEnvironment;
        readonly ILoggerFactory _loggerFactory;
        BootloaderResult _bootResult;

        public Startup(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            _hostingEnvironment = hostingEnvironment;
            _loggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //while(!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(10);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddDataProtection()
                .AddKeyManagementOptions(_ => {
                    
                })
                .SetApplicationName("Dolittle.Sentry")
                .PersistKeysToFileSystem(new DirectoryInfo(".cookies"));

            ConfigureAuthentication(services);
            ConfigureIdentityServer(services);

            _bootResult = services.AddDolittle(_ => {
                _.ExecutionContextSetup.TenantIdHeaderName = "Owner-Tenant-ID";
                //_.ExecutionContextSetup.SkipAuthentication = true;
            }, _loggerFactory);
        }

        void ConfigureAuthentication(IServiceCollection services)
        {
            var authentication = services.AddAuthentication(_ => {
                _.DefaultScheme = Constants.CompositeSchemeName;
                _.DefaultAuthenticateScheme = Constants.CompositeSchemeName;
                _.DefaultForbidScheme = Constants.CompositeSchemeName;
                _.DefaultSignInScheme = Constants.InternalCookieSchemeName;
                _.DefaultSignOutScheme = Constants.InternalCookieSchemeName;
            });
            authentication.AddCookie(Constants.InternalCookieSchemeName);
            authentication.AddCookie(Constants.ExternalCookieSchemeName);
            authentication.AddIdentityToken(Constants.IdentityTokenSchemeName);
            authentication.AddComposite(Constants.CompositeSchemeName);
        }

        void ConfigureIdentityServer(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();
            services.AddTransient<IdentityServerOptions>(serviceProvider => {
                var optionsProvider = serviceProvider.GetRequiredService<IIdentityServerOptionsProvider>();
                return optionsProvider.GetOptions();
            });
            services.AddHttpClient();

            var server = services.AddIdentityServerBuilder();
            // server.AddCookieAuthentication();
            server.AddCoreServices();
            server.AddDefaultEndpoints();
            server.AddPluggableServices();
            server.AddValidators();
            server.AddResponseGenerators();
            server.AddDefaultSecretParsers();
            server.AddDefaultSecretValidators();

            server.AddInMemoryPersistedGrants();
            server.AddResourceStore<ResourceStore>();
            server.AddClientStore<ClientStore>();
            
            // }).AddResourceStore<ResourceStore>().AddClientStore<ClientStore>();
            // services.AddSingleton<IKeyMaterialService, KeyMaterialService>();
        }


        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddDolittle(_bootResult.Assemblies, _bootResult.Bindings);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });
            app.UsePortalContext();
            app.UseDolittle();
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}