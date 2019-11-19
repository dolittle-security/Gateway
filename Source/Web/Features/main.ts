/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import 'aurelia-dependency-injection';
import { Aurelia } from 'aurelia-framework';
import environment from './environment';
import { PLATFORM } from 'aurelia-pal';
import { QueryCoordinator } from '@dolittle/queries';
import { QueryCoordinatorMock } from './QueryCoordinatorMock';
import { registerProvidersForPortal, registerAvailableTenants } from './MockData';

require('../Styles/style.scss');

export function configure(aurelia: Aurelia) {
  aurelia.use.standardConfiguration();

  if (environment.debug) {
    aurelia.use.developmentLogging();
    const queryCoordinatorMock = new QueryCoordinatorMock();
    registerProvidersForPortal(queryCoordinatorMock);
    registerAvailableTenants(queryCoordinatorMock);
    aurelia.container.registerInstance(QueryCoordinator, queryCoordinatorMock);
  }
  
  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('App')));
}