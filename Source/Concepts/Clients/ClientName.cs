/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.Clients
{
    public class ClientName : ConceptAs<string>
    {
        public static implicit operator ClientName(string name) => new ClientName { Value = name };
    }
}
