/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { autoinject } from 'aurelia-dependency-injection';
import { QueryCoordinator } from '@dolittle/queries';
import { Provider } from './Provider';
import { ExternalProvider } from './ExternalProvider';

@autoinject
export class SignedOut {¯
  externalProvider: Provider | undefined;
  providerId: string = '';

  constructor(private _queryCoordinator: QueryCoordinator) {
    
  }

  activate(params: any) {
    this.providerId = params.idp || '';
    this.populate();
  }

  async populate() {
    let query = new ExternalProvider();
    query.providerId = this.providerId;
    let result = await this._queryCoordinator.execute(query);
    if (result.items.length == 1) {
      this.externalProvider = result.items[0] as any;
    }
  }
}
