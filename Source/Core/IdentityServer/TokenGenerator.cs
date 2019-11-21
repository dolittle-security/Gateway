/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using static IdentityModel.OidcConstants;

namespace Core.IdentityServer
{
    public class TokenGenerator : ITokenService
    {
        readonly ISystemClock _clock;
        readonly ITokenCreationService _tokenCreator;
        readonly ISecureDataFormat<AuthenticationTicket> _ticketDataFormat;

        public TokenGenerator(ISystemClock clock, ITokenCreationService tokenCreator, IDataProtectionProvider dataProtectionProvider)
        {
            _clock = clock;
            _tokenCreator = tokenCreator;
            var dataProtector = dataProtectionProvider.CreateProtector("Dolittle.Security");
            _ticketDataFormat = new TicketDataFormat(dataProtector);
        }

        public Task<Token> CreateIdentityTokenAsync(TokenCreationRequest request)
        {
            request.Validate();
            return Task.FromResult(new Token(TokenTypes.IdentityToken)
            {
                CreationTime = _clock.UtcNow.UtcDateTime,
                Lifetime = request.ValidatedRequest.Client.IdentityTokenLifetime,
                Claims = request.Subject.Claims.ToList(),
                ClientId = request.ValidatedRequest.ClientId,
                AccessTokenType = request.ValidatedRequest.AccessTokenType,
                Audiences = new List<string> { request.ValidatedRequest.ClientId },
            });
        }

        public Task<Token> CreateAccessTokenAsync(TokenCreationRequest request)
        {
            request.Validate();
            return Task.FromResult(new Token(TokenTypes.AccessToken)
            {
                CreationTime = _clock.UtcNow.UtcDateTime,
                Lifetime = request.ValidatedRequest.Client.AccessTokenLifetime,
                Claims = request.Subject.Claims.ToList(),
                ClientId = request.ValidatedRequest.ClientId,
                AccessTokenType = request.ValidatedRequest.AccessTokenType,
                Audiences = new List<string> { request.ValidatedRequest.ClientId },
            });
        }


        public async Task<string> CreateSecurityTokenAsync(Token token)
        {
            switch (token.Type)
            {
                case TokenTypes.AccessToken:
                return ProtectAccessToken(token);

                case TokenTypes.IdentityToken:
                return await _tokenCreator.CreateTokenAsync(token);

                default:
                throw new InvalidOperationException($"Invalid token type {token.Type}.");
            }
        }

        string ProtectAccessToken(Token token)
        {
            var principal = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "Dolittle.Security"));
            var ticket = new AuthenticationTicket(principal, Constants.InternalCookieSchemeName);
            var cookieValue = _ticketDataFormat.Protect(ticket);
            return cookieValue;
        }
    }
}