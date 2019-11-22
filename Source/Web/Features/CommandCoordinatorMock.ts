/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

import { ICommandCoordinator, CommandResponse, ICommand } from '@dolittle/commands';

export class CommandCoordinatorMock implements ICommandCoordinator
{
    static failMe = false;
    
    handle(command: ICommand): Promise<CommandResponse> {
        console.log(`Handling command ${command}`);
        let commandResponse = {
            success: !CommandCoordinatorMock.failMe,
        } as CommandResponse;

        return Promise.resolve(commandResponse);
    }
    
}