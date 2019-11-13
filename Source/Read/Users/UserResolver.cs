/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using System.Security.Claims;
using Concepts.Claims;
using Dolittle.Logging;
using MongoDB.Driver;

namespace Read.Users
{
    public class UserResolver : ICanResolveUserForProviderSubjects
    {
        readonly IMongoCollection<User> _users;
        readonly ILogger _logger;

        public UserResolver(IMongoCollection<User> users, ILogger logger)
        {
            _users = users;
            _logger = logger;
        }

        public bool TryGetUserFor(IssuerClaim issuer, SubjectClaim subject, out User user)
        {
            user = null;
            var builder = Builders<IssuerSubjectPair>.Filter;
            var elementFilter = builder.Eq(_ => _.Issuer, issuer) & builder.Eq(_ => _.Subject, subject);
            var filter = Builders<User>.Filter.ElemMatch(_ => _.Mappings, elementFilter);

            var userMappings = _users.Find(filter).ToEnumerable();
            switch (userMappings.Count())
            {
                case 0:
                return false;

                case 1:
                user = userMappings.First();
                return true;

                default:
                _logger.Error($"Multiple possible user mappings for issuer:'{issuer}' subject:'{subject}' was found. Cannot pick one - authentication will fail.");
                return false;
            }
        }

        public bool TryGetUserFor(ClaimsPrincipal principal, out User user)
        {
            if (principal.TryGetIssuerSubjectClaims(out var issuer, out var subject))
            {
                return TryGetUserFor(issuer, subject, out user);
            }
            else
            {
                user = null;
                return false;
            }
        }
    }
}