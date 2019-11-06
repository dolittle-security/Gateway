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

namespace Core.TokenValidation
{
    public class LocalBearerTokenAuthenticationHandler : AuthenticationHandler<LocalBearerTokenAuthenticationOptions>
    {
        readonly ITokenValidator _tokenValidator;

        public LocalBearerTokenAuthenticationHandler(IOptionsMonitor<LocalBearerTokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ITokenValidator tokenValidator)
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
                new ClaimsPrincipal(new ClaimsIdentity(validationResult.Claims, "Dolittle.Bearer")),
                new AuthenticationProperties(),
                Scheme.Name
            ));
        }
    }
}