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
            _logger.Warning("Inserting portal document");

            /*
            _portals.InsertOne(new Portal
            {
                Id = Guid.Parse("22222222-1a6f-40d7-b2fc-796dba92dc44"),
                DisplayName = "The cool portal",
                Providers = new List<IdentityProviderForChoosing>() {
 new IdentityProviderForChoosing {
 Id = Guid.Parse("9cf6d6a1-056b-4931-8ebb-8fefb1c60608"),
 Name = "DolittleAD"
 },
 },
            });

            _config.InsertOne(new OpenIDConnectConfiguration
            {
                Id = Guid.Parse("9cf6d6a1-056b-4931-8ebb-8fefb1c60608"),
                Authority = "https://login.microsoftonline.com/381088c1-de08-4d18-9e60-bbe2c94eccb5",
                ClientId = "7cc1ec4d-b850-4f93-bbca-b3084a7f03a5",
                ClientSecret = "GEX[u.vmKvCK8kwVZ[hcP1CWjZ_Iwy07",
                StaticClaims = new Dictionary<ClaimType, ClaimValue>() {
 {"hello", "world"},
 },
                ClaimMapping = new Dictionary<ClaimType, ClaimType>() {
 {"name", "cool"},
 },
            });
            */


            /* --------- GA ---------- */
            /* 
            _logger.Information("Inserting mapping document");
            _mappings.InsertOne(new ProviderSubjectTenantsForMapping {
            Provider = "9cf6d6a1-056b-4931-8ebb-8fefb1c60608",
            Subject = "x0p9I9Uxpt7d3m4OazcdxqiYRfaqvTxO_LumW3zoglI",
            Portal = Guid.Parse("22222222-1a6f-40d7-b2fc-796dba92dc44"),
            Tenants = new List<TenantId>() {
            Guid.Parse("00fa79c4-9b95-43c1-8058-bb6e01e2ecab"),
            Guid.Parse("7a888183-b147-4ce3-bd4d-118133f13e4b"),
            },
            });
            */

            /*
            _logger.Information("Inserting user document");
            _users.InsertOne(new User {
            Id = Guid.NewGuid(),
            Mappings = new [] {
            new ProviderSubjectPair {
            Provider = "9cf6d6a1-056b-4931-8ebb-8fefb1c60608",
            Subject = "x0p9I9Uxpt7d3m4OazcdxqiYRfaqvTxO_LumW3zoglI",
            },
            },
            });
            */

            /* ----------- Ja ------------ */
            /*
            _logger.Information("Inserting mapping document");
            _mappings.InsertOne(new ProviderSubjectTenantsForMapping {
            Provider = "9cf6d6a1-056b-4931-8ebb-8fefb1c60608",
            Subject = "-kGwYxjJq7q_PtV4M8qiHkijVwikRTD9ec-wSwXmLZo",
            Portal = Guid.Parse("22222222-1a6f-40d7-b2fc-796dba92dc44"),
            Tenants = new List<TenantId>() {
            Guid.Parse("00fa79c4-9b95-43c1-8058-bb6e01e2ecab"),
            Guid.Parse("7a888183-b147-4ce3-bd4d-118133f13e4b"),
            },
            });

            _logger.Information("Inserting user document");
            _users.InsertOne(new User
            {
                Id = Guid.NewGuid(),
                Mappings = new[] {
 new ProviderSubjectPair {
 Provider = "9cf6d6a1-056b-4931-8ebb-8fefb1c60608",
 Subject = "-kGwYxjJq7q_PtV4M8qiHkijVwikRTD9ec-wSwXmLZo",
 },
 },


            /* ----------- EI ------------ */
            /*            
            _logger.Information("Inserting mapping document");
            _mappings.InsertOne(new ProviderSubjectTenantsForMapping {
            Provider = "9cf6d6a1-056b-4931-8ebb-8fefb1c60608",
            Subject = "hzVHVHS1TCO4NJ1zTVBDQWS8uxQoM7PLHQ75NV9pP98",
            Portal = Guid.Parse("22222222-1a6f-40d7-b2fc-796dba92dc44"),
            Tenants = new List<TenantId>() {
            Guid.Parse("00fa79c4-9b95-43c1-8058-bb6e01e2ecab"),
            Guid.Parse("7a888183-b147-4ce3-bd4d-118133f13e4b"),
            },
            });
            */
            _logger.Information("Inserting user document");
            _users.InsertOne(new User
            {
                Id = Guid.NewGuid(),
                Mappings = new[] {
 new ProviderSubjectPair {
 Provider = "9cf6d6a1-056b-4931-8ebb-8fefb1c60608",
 Subject = "hzVHVHS1TCO4NJ1zTVBDQWS8uxQoM7PLHQ75NV9pP98",
 },
 },

            });
        }
    }
}
