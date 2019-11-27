/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { containerless, bindable } from 'aurelia-framework';
import './AppContent.scss';

@containerless()
export class AppContent {
  @bindable title: string = '';
  @bindable description: string = '';
}
