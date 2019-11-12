/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Providers.Configuring
{
    public class OpenIDConnectConfiguration : IdentityProviderConfiguration
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public OpenIDConnectClientAuthenticationMethod AuthenticationMethod { get; set; }
    }
}