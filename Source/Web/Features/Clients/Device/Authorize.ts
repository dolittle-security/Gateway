/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { autoinject } from 'aurelia-dependency-injection';
import { CommandCoordinator } from '@dolittle/commands';
import { AuthorizeDeviceWithUserCode } from './AuthorizeDeviceWithUserCode';
import { Router } from 'aurelia-router';
import { CommandCoordinatorMock } from '../../CommandCoordinatorMock';
import './Authorize.scss';

@autoinject
export class Authorize {
  private _userCode = '';

  constructor(private _router: Router, private _commandCoordinator: CommandCoordinator) {}

  activate(params: any) {
    if (params.userCode) {
      this._userCode = params.userCode;
    }
  }

  async authorize() {
    let command = new AuthorizeDeviceWithUserCode();
    command.userCode = this._userCode;
    CommandCoordinatorMock.failMe = true;
    let commandResult = await this._commandCoordinator.handle(command);
    if (commandResult.success) {
      this._router.navigate('/signin/device/success');
    } else this._router.navigate('/signin/device/failed');
  }
}
