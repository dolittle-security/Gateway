/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.ReadModels;

namespace Read.SignOut
{
    public class Provider : IReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}