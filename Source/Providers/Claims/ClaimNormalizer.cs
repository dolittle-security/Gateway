/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using Concepts.Providers;
using Dolittle.Collections;
using Read.Providers.Configuring;

namespace Providers.Claims
{
    public class ClaimNormalizer : IClaimNormalizer
    {
        readonly ICanProvideIdentityProviderConfigurations _configurations;

        public ClaimNormalizer(ICanProvideIdentityProviderConfigurations configurations)
        {
            _configurations = configurations;
        }
        public ClaimsPrincipal Normalize(IdentityProviderId providerId, ClaimsPrincipal original)
        {
            var configuration = _configurations.GetCommonProviderConfiguration(providerId);
            var identity = new ClaimsIdentity("Dolittle.Sentry");

            identity.AddClaim("iss", GetSingleRequiredClaimOrThrow(original, "iss"));
            identity.AddClaim("sub", GetSingleRequiredClaimOrThrow(original, "sub"));
            identity.AddClaim("aud", GetSingleRequiredClaimOrThrow(original, "aud"));

            foreach ((var from, var to) in configuration.ClaimMapping)
            {
                original.Claims.Where(_ => _.Type == from).ForEach(_ => {
                    identity.AddClaim(to, _.Value);
                });
            }

            foreach ((var type, var value) in configuration.StaticClaims)
            {
                identity.AddClaim(type, value);
            }

            return new ClaimsPrincipal(identity);
        }

        string GetSingleRequiredClaimOrThrow(ClaimsPrincipal principal, string claimType)
        {
            var matches = principal.Claims.Where(_ => _.Type == claimType);
            switch (matches.Count())
            {
                case 0:
                throw new RequiredClaimNotPresentInPrincipal(claimType);

                case 1:
                return matches.First().Value;

                default:
                throw new MultipleClaimsForSingleRequiredClaimPresentInPrincipal(claimType);
            }
        }
    }
}