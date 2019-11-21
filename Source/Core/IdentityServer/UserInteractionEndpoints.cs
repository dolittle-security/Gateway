/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using IdentityServer4.Configuration;

namespace Core.IdentityServer
{
    public class UserInteractionEndpoints : ICanConfigureIdentityServer
    {
        public void Configure(IdentityServerOptions options)
        {
            options.UserInteraction.DeviceVerificationUrl = "/signin/device";
            options.UserInteraction.DeviceVerificationUserCodeParameter = "userCode";
        }
    }
}