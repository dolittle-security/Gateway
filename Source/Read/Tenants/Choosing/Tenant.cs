/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.ReadModels;
using Dolittle.Tenancy;

namespace Read.Tenants.Choosing
{
    public class Tenant : IReadModel
    {
        public TenantId Id { get; set; }

        public string Name { get; set; }
    }
}