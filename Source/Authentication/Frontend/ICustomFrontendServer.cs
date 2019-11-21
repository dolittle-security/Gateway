/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Mvc;

namespace Authentication.Frontend
{
    public interface ICustomFrontendServer
    {
        IActionResult ServeFile(string file);
    }
}