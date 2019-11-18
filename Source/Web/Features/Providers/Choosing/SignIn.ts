/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { autoinject } from 'aurelia-dependency-injection';
import { QueryCoordinator } from '@dolittle/queries';
import { ProvidersForPortal } from './ProvidersForPortal';
import { IdentityProviderForChoosing } from './IdentityProviderForChoosing';

@autoinject
export class SignIn {
  providers:IdentityProviderForChoosing[] = [];
  redirectUrl:string = '';

  constructor(private _queryCoordinator:QueryCoordinator) { 
    this.populate()
  }

  activate(params:any) {
    this.redirectUrl = params.rd||'/';
  }

  private async populate() {
    let query = new ProvidersForPortal();
    let result = await this._queryCoordinator.execute(query);
    this.providers = result.items as any;
  }
}
