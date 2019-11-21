/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using IdentityServer4.Hosting;
using Microsoft.AspNetCore.Http;

namespace Core.IdentityServer
{
    public class UnauthorizedResult : IEndpointResult
    {
        private readonly string _redirect;

        public UnauthorizedResult(string redirect)
        {
            _redirect = redirect;
        }

        public Task ExecuteAsync(HttpContext context)
        {
            context.Response.Redirect($"/signin?rd={_redirect}");
            return Task.CompletedTask;
        }
    }
}