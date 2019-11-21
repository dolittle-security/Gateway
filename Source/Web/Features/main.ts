/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import 'aurelia-dependency-injection';
import { Aurelia } from 'aurelia-framework';
import * as environment from './environment.json'
import { PLATFORM } from 'aurelia-pal';
import { QueryCoordinator } from '@dolittle/queries';
import { QueryCoordinatorMock } from './QueryCoordinatorMock';
import { registerProvidersForPortal, registerAvailableTenants } from './MockData';

require('../Styles/style.scss');

export function configure(aurelia: Aurelia) {
  aurelia.use.standardConfiguration().feature(PLATFORM.moduleName('Components/index'));

  if (environment.debug) {
    aurelia.use.developmentLogging();
    const queryCoordinatorMock = new QueryCoordinatorMock();
    registerProvidersForPortal(queryCoordinatorMock);
    registerAvailableTenants(queryCoordinatorMock);
    aurelia.container.registerInstance(QueryCoordinator, queryCoordinatorMock);
  } else {
    QueryCoordinator.apiBaseUrl = '/api/Dolittle/Security/Gateway/';
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
