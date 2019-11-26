import * as express from 'express';
import { Guid } from 'guid-typescript';
import { Configuration } from './Configuration';
import { ConfigureMongoDB } from './Mongo';

const configuration: Configuration = {
    Tenants: [{
        Id: Guid.parse('2c9c9f27-4136-4484-91a1-8c02b6a16d0e'),
        Name: 'Hello',
        ReadModelDatabase: 'owner_read_models',
    }],
    Portals: [{
        Id: Guid.parse('9e18444e-35e1-41ff-8e21-7ebcf7f77e5a'),
        Owner: Guid.parse('2c9c9f27-4136-4484-91a1-8c02b6a16d0e'),
        DisplayName: 'Main portal',
        Providers: [{
            Id: Guid.parse('2df82250-50c6-414b-afe3-cbfada2be036'),
            Name: 'Provider 1',
            StaticClaims: new Map([
                ['hello','world'],
            ]),
            ClaimMapping: new Map([
                ['name','cool']
            ]),
        }],
    }],
};

(async () => {
    await ConfigureMongoDB(configuration);

    const server = express();

    server.listen(8080, () => {
        console.log('Serving on http://localhost:8080/');
    });
})();


