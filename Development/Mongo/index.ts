import { MongoClient, Db } from 'mongodb';
import { Configuration, Portal, Provider } from '../Configuration';
import { Guid } from 'guid-typescript';

function GuidToBinary(guid: Guid): Buffer
{
    const buf = Buffer.from(guid.toString().replace(/-/g,''), 'hex');
    buf.slice(0, 4).reverse();
    buf.slice(4, 6).reverse();
    buf.slice(6, 8).reverse();
    return buf;
}

function GetTenantDatabase(tenant: Guid, configuration: Configuration, client: MongoClient): Db
{
    const tenantConfig = configuration.Tenants.find((t) =>  tenant.equals(t.Id));
    return client.db(tenantConfig.ReadModelDatabase);
};

async function InsertPortal(portal: Portal, database: Db): Promise<void>
{
    const collection = database.collection('Read.Providers.Choosing.Portal');
    console.log(`Inserting portal ${portal.DisplayName} for tenant ${portal.Owner}`);
    await collection.replaceOne({ _id: GuidToBinary(portal.Id) },{
        _id: GuidToBinary(portal.Id),
        DisplayName: portal.DisplayName,
        Providers: portal.Providers.map((provider) => { return {
            _id: GuidToBinary(provider.Id),
            Name: provider.Name,
        }}),
    }, { upsert: true });
};

async function InsertProviders(providers: Provider[], database: Db): Promise<void>
{
    const collection = database.collection('Read.Providers.Configuring.OpenIDConnectConfiguration');
    for (const provider of providers) {
        console.log(`Inserting provider ${provider.Name} - ${provider.Id}`);
        await collection.replaceOne({ _id: GuidToBinary(provider.Id) },{
            _id: GuidToBinary(provider.Id),
            StaticClaims: provider.StaticClaims,
            ClaimMapping: provider.ClaimMapping,
            ClientId: provider.Id.toString(),
            ClientSecret: provider.Id.toString(),
            Authority: 'authority',
            AuthenticationMethod: 0
        }, { upsert: true });
    }
};

export async function ConfigureMongoDB(configuration: Configuration): Promise<void>
{
    console.log('Configuring MongoDB...');

    const client = await new MongoClient('mongodb://localhost:27017', {
        useUnifiedTopology: true,
    }).connect();

    for (const portal of configuration.Portals) {
        const portalOwnerDb = GetTenantDatabase(portal.Owner, configuration, client);
        await InsertPortal(portal, portalOwnerDb);
        await InsertProviders(portal.Providers, portalOwnerDb);
    }
};