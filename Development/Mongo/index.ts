import { MongoClient, Db, Binary } from 'mongodb';
import { Configuration, Portal, Provider } from '../Configuration';
import { Guid } from 'guid-typescript';

function GuidToBinary(guid: Guid): Binary
{
    const buf = Buffer.from(guid.toString().replace(/-/g,''), 'hex');
    buf.slice(0, 4).reverse();
    buf.slice(4, 6).reverse();
    buf.slice(6, 8).reverse();
    return new Binary(buf, 0x03);
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
            Authority: `http://localhost:${provider.Port}`,
            AuthenticationMethod: 0
        }, { upsert: true });
    }
};

function GetUserToTenantMappings(configuration: Configuration): any
{
    const mappingsPerPortalOwner = {};
    for (const portal of configuration.Portals)
    {
        const mappings = [];
        for (const tenant of configuration.Tenants)
        {
            for (const user of tenant.Users)
            {
                for (const mapping of user.Mappings)
                {
                    const index = mappings.findIndex((m) => {
                        return m.Portal == portal.Id && m.Provider == mapping.Provider && m.Subject == mapping.Subject;
                    });
                    if (index >= 0)
                    {
                        mappings[index].Tenants.push(tenant.Id);
                    }
                    else
                    {
                        mappings.push({
                            Portal: portal.Id,
                            Provider: mapping.Provider,
                            Subject: mapping.Subject,
                            Tenants: [tenant.Id],
                        });
                    }
                }
            }
        }
        mappingsPerPortalOwner[portal.Owner.toString()] = mappings.map((mapping) => { return {
            Portal: GuidToBinary(mapping.Portal),
            Provider: mapping.Provider.toString(),
            Subject: mapping.Subject,
            Tenants: mapping.Tenants.map((t) => GuidToBinary(t)),
        }});
    }
    return mappingsPerPortalOwner;
}

async function InsertUsers(configuration: Configuration, client: MongoClient): Promise<void>
{
    for (const tenant of configuration.Tenants) {
        const tenantDb = GetTenantDatabase(tenant.Id, configuration, client);
        
        const userCollection = tenantDb.collection('Read.Users.User');
        for (const user of tenant.Users) {
            console.log(`Inserting user ${user.Id} into tenant ${tenant.Id}`);
            await userCollection.replaceOne({ _id: GuidToBinary(user.Id) },{
                _id: GuidToBinary(user.Id),
                Mappings: user.Mappings.map((mapping) => { return {
                    Provider: mapping.Provider.toString(),
                    Subject: mapping.Subject,
                }}),
            },{ upsert: true });
        }
    }
    const mappingsPerPortal = GetUserToTenantMappings(configuration);
    for (const portalOwner in mappingsPerPortal) {
        if (mappingsPerPortal.hasOwnProperty(portalOwner)) {
            const tenantId = Guid.parse(portalOwner);
            const tenantDb = GetTenantDatabase(tenantId, configuration, client);
            const mappingCollection = tenantDb.collection('Read.Portals.UserMapping.ProviderSubjectTenantsForMapping');
            try
            {
                await mappingCollection.drop();
            }
            catch {}
            const mappings = mappingsPerPortal[portalOwner];
            await mappingCollection.insertMany(mappings);
        }
    }
}

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
    await InsertUsers(configuration, client);
};