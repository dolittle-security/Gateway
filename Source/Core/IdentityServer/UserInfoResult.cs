/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Hosting;
using Microsoft.AspNetCore.Http;

namespace Core.IdentityServer
{
    public class UserInfoResult : IEndpointResult
    {
        readonly IEnumerable<Claim> _claims;

        public UserInfoResult(IEnumerable<Claim> claims)
        {
            _claims = claims;
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.SetNoCache();
            var distinctClaims = _claims.Distinct(new ClaimComparer());
            var dictionary = new Dictionary<string, object>();
            foreach (var claim in distinctClaims)
            {
                if (!dictionary.ContainsKey(claim.Type))
                {
                    dictionary.Add(claim.Type, GetValue(claim));
                }
                else
                {
                    var value = dictionary[claim.Type];
                    if (value is List<object> list)
                    {
                        list.Add(GetValue(claim));
                    }
                    else
                    {
                        dictionary[claim.Type] = new List<object> { value, GetValue(claim) };
                    }
                }
            }
            await context.Response.WriteJsonAsync(dictionary);
        }

        private object GetValue(Claim claim)
        {
            return claim.Value;
        }
    }
}