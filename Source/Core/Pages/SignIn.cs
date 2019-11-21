/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Threading.Tasks;
using Authentication;
using Authentication.Frontend;
using Dolittle.Configuration;
using Dolittle.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Read.Portals.UserMapping;
using Read.Providers.Choosing;

namespace Core.Pages
{
    [Route("SignIn")]
    public class SignIn : ControllerBase
    {
        readonly ICanResolveProvidersForChoosing _resolver;
        readonly ICanResolveTenantsForProviderSubjects _mapper;
        readonly ICustomFrontendServer _server;
        readonly IAuthenticationFrontend _frontend;
        readonly ICanTriggerRemoteAuthentication _remoteAuthenticator;
        readonly ICanSignUserInToTenant _tenantAuthenticator;
        readonly ICanHandleDeviceAuthorization _deviceAuthorizer;
        readonly ILogger _logger;

        public SignIn(
            ICanResolveProvidersForChoosing resolver,
            ICanResolveTenantsForProviderSubjects mapper,
            ICustomFrontendServer server,
            IAuthenticationFrontend frontend,
            ICanTriggerRemoteAuthentication remoteAuthenticator,
            ICanSignUserInToTenant tenantAuthenticator,
            ICanHandleDeviceAuthorization deviceAuthorizer,
            ILogger logger
        )
        {
            _resolver = resolver;
            _mapper = mapper;
            _server = server;
            _frontend = frontend;
            _remoteAuthenticator = remoteAuthenticator;
            _tenantAuthenticator = tenantAuthenticator;
            _deviceAuthorizer = deviceAuthorizer;
            _logger = logger;
        }

        [HttpGet("{*file}")]
        public IActionResult ServeFile(string file)
        {
            return _server.ServeFile(file);
        }

        [HttpGet]
        public IActionResult Signin(string rd)
        {
            var providers = _resolver.AllAvailableIdentityProvidersForChoosing();
            switch (providers.Count())
            {
                case 0:
                return _frontend.NoProvidersAvailable(HttpContext);

                case 1:
                return _remoteAuthenticator.Challenge(HttpContext, providers.First().Id, rd);

                default:
                return _frontend.ChooseProvider(HttpContext);
            }
        }

        [HttpGet("Error")]
        public IActionResult Error()
        {
            return _frontend.Error(HttpContext);
        }

        [HttpGet("Tenant")]
        public async Task<IActionResult> SelectTenant(string rd)
        {
            var authResult = await HttpContext.AuthenticateAsync(Constants.ExternalCookieSchemeName);
            if (authResult.Succeeded)
            {
                var userTenants = _mapper.GetTenantsFor(authResult.Principal);
                switch (userTenants.Count())
                {
                    case 0:
                    return _frontend.NoTenantsForUser(HttpContext);

                    case 1:
                    return _tenantAuthenticator.SignUserInTo(HttpContext, userTenants.First(), rd);

                    default:
                    return _frontend.ChooseTenant(HttpContext);
                }
            }
            return Signin(rd);
        }

        [HttpGet("Device")]
        public async Task<IActionResult> Device(string userCode)
        {
            var authResult = await HttpContext.AuthenticateAsync(Constants.InternalCookieSchemeName);
            if (authResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(userCode))
                {
                    return await _deviceAuthorizer.HandleAsync(HttpContext, userCode);
                }
                else
                {
                    return _frontend.DeviceSignIn(HttpContext);
                }
            }
            return Signin("/signin/device?userCode="+userCode);
        }
    }
}