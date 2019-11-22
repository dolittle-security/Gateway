/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Commands;

namespace Domain.Clients.Device
{
    public class AuthorizeDeviceWithUserCode : ICommand
    {
        public string UserCode { get; set; }
    }
}