/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Context;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Frontend
{
    public class CustomFrontendServer : ICustomFrontendServer
    {
        readonly IPortalContextManager _portalManager;
        readonly IFileSystem _sharedFiles;
        readonly IFiles _tenantFiles;

        public CustomFrontendServer(IPortalContextManager portalManager, IFileSystem sharedFiles, IFiles tenantFiles)
        {
            _portalManager = portalManager;
            _sharedFiles = sharedFiles;
            _tenantFiles = tenantFiles;
        }

        public IActionResult ServeFile(string file)
        {
            if (TryGetCustomOrDefaultFile(file, out var contents))
            {
                return new ContentResult
                {
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "text/html; charset=UTF-8",
                    Content = contents,
                };
            }
            else
            {
                return new NotFoundResult();
            }
        }

        bool TryGetCustomOrDefaultFile(string file, out string contents)
        {
            contents = "";
            var portalId = _portalManager.Current.Portal;
            if (_tenantFiles.DirectoryExists($"{portalId}"))
            {
                try
                {
                    contents = _tenantFiles.ReadAllText($"{portalId}/{file}");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                contents = _sharedFiles.ReadAllText($"./wwwroot/{file}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}