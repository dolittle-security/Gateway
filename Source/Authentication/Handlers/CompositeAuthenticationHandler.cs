/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authentication.Handlers
{
    public class CompositeAuthenticationHandler : AuthenticationHandler<CompositeAuthenticationOptions>
    {
        static readonly string[] schemes = { Constants.InternalCookieSchemeName, Constants.IdentityTokenSchemeName };

        public CompositeAuthenticationHandler(IOptionsMonitor<CompositeAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var results = await AuthenticateMultiple(schemes);
            var succesful = results.Where(_ => _.Succeeded);
            
            if (succesful.Count() < 1)
            {
                return AuthenticateResult.NoResult();
            }
            else if (succesful.Count() > 1 && MultipleIdentitiesThatAreNotEqual(succesful))
            {
                return AuthenticateResult.Fail(new MultipleAuthenticatedIdentitiesWithConflictingClaims());
            }
            else
            {
                return succesful.First();
            }
        }

        async Task<IEnumerable<AuthenticateResult>> AuthenticateMultiple(IEnumerable<string> schemes)
        {
            var results = new List<AuthenticateResult>();
            foreach (var scheme in schemes)
            {
                results.Add(await Context.AuthenticateAsync(scheme));
            }
            return results;
        }

        bool MultipleIdentitiesThatAreNotEqual(IEnumerable<AuthenticateResult> succesful)
        {
            var sortedClaims = succesful.Select(_ => _.Principal.Claims.Select(c => c.ToString()).OrderBy(s => s));

            var first = sortedClaims.First();
            foreach (var other in sortedClaims.Skip(1))
            {
                if (!first.SequenceEqual(other))
                {
                    return true;
                }
            }
            return false;
        }
    }
}