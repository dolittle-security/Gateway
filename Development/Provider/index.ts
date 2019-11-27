import { Provider as OidcProvider } from 'oidc-provider';
import { Configuration, Provider as ProviderConfiguration, Portal } from '../Configuration';

export class Provider
{
    private _configuration: ProviderConfiguration;
    private _provider: OidcProvider;

    constructor(configuration: ProviderConfiguration, portal: Portal)
    {
        this._configuration = configuration;
        this._provider = new OidcProvider(`http://localhost:${configuration.Port}`, {
            features: {
                devInteractions: {
                    enabled: true,
                },
            },
            clients: [{
                client_id: configuration.Id.toString(),
                redirect_uris: [`http://${portal.Domain}:8080/dolittle/security/gateway/signin/oidc-${configuration.Id.toString()}`],
                client_secret: configuration.Id.toString(),
            }],
            findAccount: (_, sub) => {
                return {
                    accountId: sub,
                    async claims(use, scope, claims, rejected) {
                        const user = configuration.Users.find((user) => user.Subject == sub);
                        if (user)
                        {
                            const userClaims = { sub: sub, };
                            for (const claim of user.Claims) {
                                userClaims[claim[0]] = claim[1];
                            }
                            return userClaims;
                        }
                        else
                        {
                            return { sub: sub, };
                        }
                    },
                };
            }
        });
    }

    async listen(port: number)
    {
        return new Promise<void>((resolve) => {
            this._provider.listen(port, () => {
                console.log(`Serving provider ${this._configuration.Name} - ${this._configuration.Id} on http://localhost:${port}`);
                resolve();
            });
        });
    }
}

export async function CreateProviders(configuration: Configuration)
{
    for (const portal of configuration.Portals) {
        for (const config of portal.Providers) {
            const provider = new Provider(config, portal);
            await provider.listen(config.Port);
        }
    }
}