import { Guid } from 'guid-typescript';

export class ProviderSubjectPair
{
    public Provider: Guid;
    public Subject: string;
}

export class InternalUser
{
    public Id: Guid;
    public Mappings: ProviderSubjectPair[];
}

export class Tenant
{
    public Id: Guid;
    public Name: string;
    public ReadModelDatabase: string;
    public Users: InternalUser[];
}

export class ExternalUser
{
    public Subject: string;
    public Claims: Map<string, string>;
}

export class Provider
{
    public Id: Guid;
    public Name: string;
    public StaticClaims: Map<string, string>;
    public ClaimMapping: Map<string, string>;
    public Users: ExternalUser[];
    public Port: number;
}

export class Portal
{
    public Id: Guid;
    public Owner: Guid;
    public Domain: string; 
    public DisplayName: string;
    public Providers: Provider[];
}

export class Configuration
{
    public Tenants: Tenant[];
    public Portals: Portal[];
}