/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Providers;

namespace Read.Providers.Configuring
{
    public class OpenIDConnectConfiguration
    {
        public IdentityProviderId Id { get; set; }
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public OpenIDConnectClientAuthenticationMethod AuthenticationMethod { get; set; }
    }
}