import { Guid } from 'guid-typescript';

export class Portal
{
    public Id: Guid;
    public Owner: Guid;
    public DisplayName: string;
    public Providers: Provider[];
}

export class Provider
{
    public Id: Guid;
    public Name: string;
    public StaticClaims: Map<string, string>;
    public ClaimMapping: Map<string, string>;
}

export class Tenant
{
    public Id: Guid;
    public Name: string;
    public ReadModelDatabase: string;
}

export class Configuration
{
    public Tenants: Tenant[];
    public Portals: Portal[];
}