using System;
using System.Collections.Generic;
using Dolittle.Logging;
using MongoDB.Driver;
using Read.Providers.Choosing;
using Read.Providers.Configuring;

namespace Setup
{
    public class Command
    {
        readonly IMongoCollection<Portal> _portals;
        readonly IMongoCollection<OpenIDConnectConfiguration> _config;
        readonly ILogger _logger;

        public Command(IMongoCollection<Portal> portals, IMongoCollection<OpenIDConnectConfiguration> config, ILogger logger)
        {
            _portals = portals;
            _config = config;
            _logger = logger;
        }

        public void Run()
        {
            _logger.Information("Nothing to see here... :)");
        }
    }
}
