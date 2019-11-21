/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.Clients;
using Concepts.Portals;

namespace Read.Clients.Configuring
{
    public abstract class Client
    {
        public ClientId Id { get; set; }
        public PortalId Portal { get; set; }
        public ClientName Name { get; set; }
        public ClientDescription Description { get; set; }
    }
}