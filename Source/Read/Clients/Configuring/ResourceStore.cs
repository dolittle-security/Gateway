/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Read.Clients.Configuring
{
    public class ResourceStore : IResourceStore
    {
        readonly Resources _resources;

        public ResourceStore()
        {
            _resources = new Resources();
            _resources.OfflineAccess = false;
            _resources.IdentityResources.Add(new IdentityResources.OpenId());
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            return Task.FromResult(_resources.ApiResources.Where(_ => _.Name == name).FirstOrDefault());
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(_resources.ApiResources.Where(_ => scopeNames.Contains(_.Name)));
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(_resources.IdentityResources.Where(_ => scopeNames.Contains(_.Name)));
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(_resources);
        }
    }
}