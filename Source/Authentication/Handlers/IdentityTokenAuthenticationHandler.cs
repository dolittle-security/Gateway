/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authentication.Handlers
{
    public class IdentityTokenAuthenticationHandler : AuthenticationHandler<IdentityTokenAuthenticationOptions>
    {
        readonly ITokenValidator _tokenValidator;

        public IdentityTokenAuthenticationHandler(IOptionsMonitor<IdentityTokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ITokenValidator tokenValidator)
            : base(options, logger, encoder, clock)
        {
            _tokenValidator = tokenValidator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorization = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authorization))
            {
                return AuthenticateResult.Fail("No Authorization Header is sent.");
            }

            var token = "";
            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("No Access Token is sent.");
            }

            var validationResult = await _tokenValidator.ValidateIdentityTokenAsync(token);
            if (validationResult.IsError)
            {
                return AuthenticateResult.Fail(validationResult.Error);
            }

            return AuthenticateResult.Success(new AuthenticationTicket(
                new ClaimsPrincipal(new ClaimsIdentity(validationResult.Claims, Scheme.Name)),
                new AuthenticationProperties(),
                Scheme.Name
            ));
        }
    }
}