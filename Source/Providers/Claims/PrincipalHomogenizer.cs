/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Security.Claims;
using Dolittle.Collections;

namespace Providers.Claims
{
    public class PrincipalHomogenizer : IPrincipalHomogenizer
    {
        public ClaimsPrincipal Homogenize(string scheme, ClaimsPrincipal original)
        {
            Console.WriteLine($"!!!!!!!!!! HOMOGENIZING FROM {scheme} !!!!!!!!!!!!");

            original.Claims.ForEach(_ => Console.WriteLine($"{_.Type}: {_.Value}"));

            return new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim("sub", original.Claims.First(_ => _.Type == "sub").Value),
            }, "Dolittle.Sentry"));
        }
    }
}