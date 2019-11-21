/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Context;
using IdentityServer4.Services;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace Read.Portals.Keys
{
    public class PortalKeyMaterialService : IKeyMaterialService
    {
        readonly PortalKeys _keys;

        public PortalKeyMaterialService(IPortalContextManager portalManager, IMongoCollection<PortalKeys> keysCollection)
        {
            var portalId = portalManager.Current.Portal;
            if (keysCollection.TryFindById(portalId, out var portalKeys))
            {
                _keys = portalKeys;
            }
            else
            {
                var portalKey = PortalKey.Create(2048);
                _keys = new PortalKeys {
                    Id = portalId,
                    SigingKey = portalKey,
                    ValidationKeys = new List<PortalKey>() { portalKey },
                };
                keysCollection.InsertOne(_keys);
            }
        }

        public Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            return Task.FromResult(new SigningCredentials(_keys.SigingKey.AsRsaSecurityKey(), "RS256"));
        }

        public Task<IEnumerable<SecurityKey>> GetValidationKeysAsync()
        {
            return Task.FromResult(_keys.ValidationKeys.Select(_ => (SecurityKey)_.AsRsaSecurityKey()));
        }
    }
}