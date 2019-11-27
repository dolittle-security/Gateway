import * as express from 'express';
import { Guid } from 'guid-typescript';
import { Configuration } from './Configuration';
import { ConfigureMongoDB } from './Mongo';
import { CreateProviders } from './Provider';
import { CreateProxy } from './Proxy';

const configuration: Configuration = {
    Tenants: [{
        Id: Guid.parse('2c9c9f27-4136-4484-91a1-8c02b6a16d0e'),
        Name: 'Hello',
        ReadModelDatabase: 'owner_read_models',
        Users: [{
            Id: Guid.parse('80a5afb8-cd28-406d-89ad-93d8b80d8b70'),
            Mappings: [{
                Provider: Guid.parse('2df82250-50c6-414b-afe3-cbfada2be036'),
                Subject: 'user1',
            }],
        }],
    }],

    Portals: [{
        Id: Guid.parse('9e18444e-35e1-41ff-8e21-7ebcf7f77e5a'),
        Owner: Guid.parse('2c9c9f27-4136-4484-91a1-8c02b6a16d0e'),
        DisplayName: 'Main portal',
        Domain: 'main.localhost',
        Providers: [{
            Id: Guid.parse('2df82250-50c6-414b-afe3-cbfada2be036'),
            Name: 'Provider 1',
            Port: 9001,
            StaticClaims: new Map([
                ['hello','world'],
            ]),
            ClaimMapping: new Map([
                ['name','cool'],
            ]),
            Users: [{
                Subject: 'user1',
                Claims: new Map([
                    ['name','User 1'],
                    ['email', 'user1@provider1.com'],
                ]),
            }],
        }],
    }],
};

(async () => {
    await ConfigureMongoDB(configuration);
    await CreateProviders(configuration);
    const server = CreateProxy(configuration);

    server.listen(8080, () => {
        console.log('Serving proxy on http://localhost:8080/');
    });
})();


