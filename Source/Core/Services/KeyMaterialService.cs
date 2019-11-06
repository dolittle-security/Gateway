/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
    public class KeyMaterialService : IKeyMaterialService
    {
        readonly RsaSecurityKey _key;
        readonly SigningCredentials _signingCredentials;

        public KeyMaterialService()
        {
            var rsa = RSA.Create();
            if (rsa is RSACryptoServiceProvider)
            {
                rsa.Dispose();
                var cng = new RSACng(2048);
                var parameters = cng.ExportParameters(true);
                _key = new RsaSecurityKey(parameters);
            }
            else
            {
                rsa.KeySize = 2048;
                _key = new RsaSecurityKey(rsa);
            }
            _key.KeyId = CryptoRandom.CreateUniqueId(16);

            _signingCredentials = new SigningCredentials(_key, "RS256");

        }

        public Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            return Task.FromResult<SigningCredentials>(_signingCredentials);
        }

        public Task<IEnumerable<SecurityKey>> GetValidationKeysAsync()
        {
            return Task.FromResult<IEnumerable<SecurityKey>>(new SecurityKey[] { _key });
        }
    }
}