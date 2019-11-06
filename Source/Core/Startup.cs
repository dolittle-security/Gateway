/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Autofac;
using Core.Services;
using Core.TokenValidation;
using Dolittle.Booting;
using Dolittle.DependencyInversion.Autofac;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Signin
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
                .SetApplicationName("Dolittle.Sentry")
                .PersistKeysToFileSystem(new DirectoryInfo(".cookies"));
            
            
            
            services.AddAuthentication(_ => {
                _.DefaultScheme = "Dolittle.Sentry";
                _.DefaultAuthenticateScheme = "Dolittle.Sentry";
                _.DefaultChallengeScheme = "Dolittle.Sentry";
                _.DefaultForbidScheme = "Dolittle.Sentry";
                _.DefaultSignInScheme = "Dolittle.Sentry";
                _.DefaultSignOutScheme = "Dolittle.Sentry";
            }).AddCookie("Dolittle.Sentry").AddScheme<LocalBearerTokenAuthenticationOptions,LocalBearerTokenAuthenticationHandler>("Dolittle.Bearer", _ => {});
            
            /*.AddLocalApi("Dolittle.Bearer", _ => {
                _.ExpectedScope = "db9a84f6-82c2-4f07-84de-5e58607c4eb5";
            });*/

            services.AddIdentityServer(_ => {
                _.UserInteraction.ErrorUrl = "/error";
                _.UserInteraction.LoginUrl = "/signin";
                _.UserInteraction.LoginReturnUrlParameter = "rd";
                _.Authentication.CookieAuthenticationScheme = "Dolittle.Sentry";
            }).AddResourceStore<ResourceStore>().AddClientStore<ClientStore>();

            services.AddSingleton<IKeyMaterialService, KeyMaterialService>();

            services.AddMvc();

            _bootResult = services.AddDolittle(_loggerFactory);
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddDolittle(_bootResult.Assemblies, _bootResult.Bindings);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}