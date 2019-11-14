/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Security.Claims;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Tenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Read.Portals.UserMapping;
using Read.Users;

namespace Authentication
{
    public class LocalAuthenticator : ICanSignUserInToTenant
    {
        readonly ICanResolveTenantsForProviderSubjects _mapper;
        readonly IExecutionContextManager _manager;
        readonly FactoryFor<ICanResolveUserForProviderSubjects> _userMapperFactory;
        readonly ICanGenerateTenantPrincipal _generator;

        public LocalAuthenticator(ICanResolveTenantsForProviderSubjects mapper, IExecutionContextManager manager, FactoryFor<ICanResolveUserForProviderSubjects> userMapperFactory, ICanGenerateTenantPrincipal generator)
        {
            _mapper = mapper;
            _manager = manager;
            _userMapperFactory = userMapperFactory;
            _generator = generator;
        }

        public IActionResult SignUserInTo(HttpContext context, TenantId tenant, string redirectUri)
        {
            var authResult = context.AuthenticateAsync(Constants.ExternalCookieSchemeName).Result;
            if (authResult.Succeeded)
            {
                var userTenants = _mapper.GetTenantsFor(authResult.Principal);
                foreach (var userTenant in userTenants)
                {
                    if (userTenant == tenant)
                    {
                        _manager.CurrentFor(tenant);
                        return SetTenantCredentialsFor(context, _userMapperFactory(), authResult.Principal, redirectUri);
                    }
                }
            }
            return new UnauthorizedResult();
        }

        IActionResult SetTenantCredentialsFor(HttpContext context, ICanResolveUserForProviderSubjects userMapper, ClaimsPrincipal portalPrincipal, string redirectUri)
        {
            if (userMapper.TryGetUserFor(portalPrincipal, out var user))
            {
                var tenantPrincipal = _generator.GenerateFor(user);
                context.SignInAsync(Constants.InternalCookieSchemeName, tenantPrincipal);
                return new RedirectResult(redirectUri, false);
            }
            return new UnauthorizedResult();
        }
    }
}