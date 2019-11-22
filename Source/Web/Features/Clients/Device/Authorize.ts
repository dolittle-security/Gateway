/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { autoinject } from 'aurelia-dependency-injection';
import { CommandCoordinator } from '@dolittle/commands';

@autoinject
export class Authorize {
  userCode: string = '';

  activate(params: any) {
  }

  authorize() {
    console.log('authorizing', this.userCode);
  }
}
