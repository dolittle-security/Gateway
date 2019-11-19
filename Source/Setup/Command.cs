using System;
using System.Collections.Generic;
using Concepts.Claims;
using Dolittle.Logging;
using Dolittle.Tenancy;
using MongoDB.Driver;
using Read.Portals.UserMapping;
using Read.Providers.Choosing;
using Read.Providers.Configuring;
using Read.Users;

namespace Setup
{
    public class Command
    {
        readonly IMongoCollection<Portal> _portals;
        readonly IMongoCollection<OpenIDConnectConfiguration> _config;
        readonly ILogger _logger;
        readonly IMongoCollection<ProviderSubjectTenantsForMapping> _mappings;
        readonly IMongoCollection<User> _users;

        public Command(
            IMongoCollection<Portal> portals,
            IMongoCollection<OpenIDConnectConfiguration> config,
            IMongoCollection<ProviderSubjectTenantsForMapping> mappings,
            IMongoCollection<User> users,
            ILogger logger)
        {
            _portals = portals;
            _config = config;
            _logger = logger;
            _mappings = mappings;
            _users = users;
        }

        public void Run()
        {
            _logger.Information("Nothing to see here... :)");
        }
    }
}
