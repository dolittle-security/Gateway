/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Autofac;
using Context;
using Core.Authentication;
using Core.Services;
using Dolittle.AspNetCore.Bootstrap;
using Dolittle.Booting;
using Dolittle.DependencyInversion.Autofac;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Dolittle.Types;
using Dolittle.Serialization.Json;
using Dolittle.Collections;
using Dolittle.Concepts.Serialization.Json;

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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddDataProtection()
                .AddKeyManagementOptions(_ => {
                    
                })
                .SetApplicationName("Dolittle.Sentry")
                .PersistKeysToFileSystem(new DirectoryInfo(".cookies"));

            
            services.AddAuthentication(_ => {
                _.DefaultScheme = CompositeAuthenticationOptions.CompositeSchemeName;
                _.DefaultAuthenticateScheme = CompositeAuthenticationOptions.CompositeSchemeName;
                _.DefaultForbidScheme = CompositeAuthenticationOptions.CompositeSchemeName;
                _.DefaultSignInScheme = CompositeAuthenticationOptions.CookieSchemeName;
                _.DefaultSignOutScheme = CompositeAuthenticationOptions.CookieSchemeName;
            }).AddCookie(CompositeAuthenticationOptions.CookieSchemeName).AddIdentityToken(CompositeAuthenticationOptions.IdentityTokenSchemeName).AddComposite(CompositeAuthenticationOptions.CompositeSchemeName);
            

            services.AddIdentityServer(_ => {
                _.UserInteraction.ErrorUrl = "/error";
                _.UserInteraction.LoginUrl = "/signin";
                _.UserInteraction.LoginReturnUrlParameter = "rd";
                //_.Authentication.CookieAuthenticationScheme = "Dolittle.Sentry";
            }).AddResourceStore<ResourceStore>().AddClientStore<ClientStore>();

            services.AddSingleton<IKeyMaterialService, KeyMaterialService>();

            _bootResult = services.AddDolittle(_ => { _.TenantIdHeaderName = "Owner-Tenant-ID"; }, _loggerFactory);

            services.AddMvc().AddJsonOptions(_ => {
               _.SerializerSettings.Converters.Add(new ConceptConverter()); 
            });
        }


        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddDolittle(_bootResult.Assemblies, _bootResult.Bindings);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UsePortalContext();
            app.UseDolittle();
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}