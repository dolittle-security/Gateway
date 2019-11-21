/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using IdentityServer4.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Core.IdentityServer
{
    public class UserInfoEndpoint : IEndpointHandler
    {

        public async Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            var authResult = await context.AuthenticateAsync();
            if (authResult.Succeeded)
            {
                return new UserInfoResult(authResult.Principal.Claims);
            }
            else
            {
                return new UnauthorizedResult("/connect/userinfo");
            }
        }
    }
}