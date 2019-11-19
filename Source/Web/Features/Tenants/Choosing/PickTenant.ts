/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { autoinject } from 'aurelia-dependency-injection';
import { QueryCoordinator } from '@dolittle/queries';
import { AvailableTenants } from './AvailableTenants';
import { Tenant } from './Tenant';
import { Router } from 'aurelia-router';

@autoinject
export class SignIn {
  tenants: Tenant[] = [];
  redirectUrl: string = '';

  constructor(private _queryCoordinator: QueryCoordinator, private _router: Router) {
    this.populate();
  }

  activate(params: any) {
    this.redirectUrl = params.rd || '';
  }

  private async populate() {
    let query = new AvailableTenants();
    let result = await this._queryCoordinator.execute(query);
    this.tenants = result.items as any;
  }
}
