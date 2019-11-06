/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Microsoft.AspNetCore.Authentication;

namespace Core.Providers
{
    public interface IDynamicIdenityProviderManager
    {
        void AddIdentityProvider<THandler,TOptions>(Guid id, TOptions options)
            where THandler : RemoteAuthenticationHandler<TOptions>
            where TOptions : RemoteAuthenticationOptions, new();

        void RemoveIdentityProvider(Guid id);
    }
}