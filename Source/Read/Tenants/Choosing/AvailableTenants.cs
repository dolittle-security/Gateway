/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Dolittle.Execution;
using Dolittle.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Read.Portals.UserMapping;
using Concepts.Claims;

namespace Read.Tenants.Choosing
{
    public class AvailableTenants : IQueryFor<Tenant>
    {
        readonly ICanResolveTenantsForProviderSubjects _mapper;
        readonly IExecutionContextManager _executionContextManager;
        readonly IHttpContextAccessor _httpContextAccessor;

        public AvailableTenants(
            ICanResolveTenantsForProviderSubjects mapper,
            IHttpContextAccessor httpContextAccessor,
            IExecutionContextManager executionContextManager)
        {
            _mapper = mapper;
            _executionContextManager = executionContextManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<Tenant> Query
        {
            get
            {
                var authResult = _httpContextAccessor.HttpContext.AuthenticateAsync("Dolittle.External").Result;
                if (authResult.Succeeded)
                {
                    if (authResult.Principal.TryGetProviderSubjectClaims(out var provider, out var subject))
                    {
                        return _mapper.GetTenantsFor(provider, subject).Select(_ =>
                            new Tenant
                            {
                                Id = _,
                                Name = _.ToString()
                            }).AsQueryable();
                    }
                }

                return new Tenant[0].AsQueryable();
            }

        }
    }
}