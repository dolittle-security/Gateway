/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Security.Claims;
using Concepts.Claims;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Tenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Providers.Claims;
using Read.Portals.UserMapping;
using Read.Users;

namespace Core.Authentication
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

        public IActionResult SignUserInTo(HttpContext context, TenantId tenant, Uri redirectUri)
        {
            var authResult = context.AuthenticateAsync("Dolittle.External").Result;
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

        IActionResult SetTenantCredentialsFor(HttpContext context, ICanResolveUserForProviderSubjects userMapper, ClaimsPrincipal portalPrincipal, Uri redirectUri)
        {
            if (userMapper.TryGetUserFor(portalPrincipal, out var user))
            {
                var tenantPrincipal = _generator.GenerateFor(user);
                context.SignInAsync("Dolittle.Cookie", tenantPrincipal);
                return new RedirectResult(redirectUri.ToString(), false);
            }
            return new UnauthorizedResult();
        }
    }
}