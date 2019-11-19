import { QueryCoordinatorMock } from "./QueryCoordinatorMock";
import { ReadModel } from "@dolittle/readmodels";

export function registerProvidersForPortal (queryCoordinator: QueryCoordinatorMock): void {
    queryCoordinator.registerQueryHandler('ProvidersForPortal', () => {
        return [{
            id: '00',
            name: 'Name',
        }] as unknown as ReadModel[];
    });
}

export function registerAvailableTenants (queryCoordinator: QueryCoordinatorMock): void {
    queryCoordinator.registerQueryHandler('AvailableTenants', () => {
        return [{
            id: '00',
            name: 'Tenant Name',
        }] as unknown as ReadModel[];
    });
}