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
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authentication.Handlers
{
    public class BearerTokenAuthenticationHandler : AuthenticationHandler<BearerTokenAuthenticationOptions>
    {
        readonly ISecureDataFormat<AuthenticationTicket> _ticketDataFormat;

        public BearerTokenAuthenticationHandler(IOptionsMonitor<BearerTokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IDataProtectionProvider dataProtectionProvider)
            : base(options, logger, encoder, clock)
        {
            var dataProtector = dataProtectionProvider.CreateProtector("Dolittle.Security");
            _ticketDataFormat = new TicketDataFormat(dataProtector);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorization = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("No Authorization Header is sent."));
            }

            var token = "";
            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }
            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("No Access Token is sent."));
            }

            var ticket = _ticketDataFormat.Unprotect(token);
            if (ticket == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Unprotect ticket failed"));
            }
            
            // TODO: More verification of the ticket
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}