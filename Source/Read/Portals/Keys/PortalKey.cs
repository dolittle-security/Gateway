/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Security.Cryptography;
using Concepts.Portals;
using Microsoft.IdentityModel.Tokens;

namespace Read.Portals.Keys
{
    public class PortalKey
    {
        public PortalKeyId Id { get; set; }
        public RSAParameters Parameters { get; set; }


        public static PortalKey Create(int keySize)
        {
            var keyId = Guid.NewGuid();
            RsaSecurityKey key;
            var rsa = RSA.Create();
            if (rsa is RSACryptoServiceProvider)
            {
                rsa.Dispose();
                var cng = new RSACng(keySize);
                key = new RsaSecurityKey(cng.ExportParameters(true));
            }
            else
            {
                rsa.KeySize = keySize;
                key = new RsaSecurityKey(rsa);
            }
            key.KeyId = keyId.ToString();
            RSAParameters parameters;
            if (key.Rsa != null)
            {
                parameters = key.Rsa.ExportParameters(true);
            }
            else
            {
                parameters = key.Parameters;
            }

            return new PortalKey {
                Id = keyId,
                Parameters = parameters,
            };
        }

        public RsaSecurityKey AsRsaSecurityKey()
        {
            return new RsaSecurityKey(Parameters) {
                KeyId = Id.ToString(),
            };
        }
    }
}