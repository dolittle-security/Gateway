/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.IO;
using Context;
using Dolittle.Execution;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Authentication.Frontend
{
    public class CustomFrontendServer : ICustomFrontendServer
    {
        const string _basePath = "./wwwroot";

        readonly IPortalContextManager _portalManager;
        readonly IFileSystem _sharedFiles;
        readonly IFiles _tenantFiles;
        readonly FileExtensionContentTypeProvider _contentTypeProvider;
        readonly IExecutionContextManager _executionContextManager;

        public CustomFrontendServer(IPortalContextManager portalManager, IFileSystem sharedFiles, IFiles tenantFiles, IExecutionContextManager executionContextManager)
        {
            _portalManager = portalManager;
            _sharedFiles = sharedFiles;
            _tenantFiles = tenantFiles;
            _contentTypeProvider = new FileExtensionContentTypeProvider();
            _executionContextManager = executionContextManager;
        }

        public IActionResult ServeFile(string file)
        {
            if (TryGetCustomOrDefaultFile(file, out var contents, out var mimetype))
            {
                return new FileContentResult(contents, mimetype);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        bool TryGetCustomOrDefaultFile(string file, out byte[] contents, out string mimetype)
        {
            var customDirectoryPath = GetTenantPortalPath();
            if (Directory.Exists(customDirectoryPath))
            {
                return TryReadFileContents(customDirectoryPath, file, out contents, out mimetype);
            }
            else
            {
                var defaultDirectoryPath = Path.Combine(_basePath, "default");
                return TryReadFileContents(defaultDirectoryPath, file, out contents, out mimetype);
            }
        }

        bool TryReadFileContents(string @base, string file, out byte[] contents, out string mimetype)
        {
            mimetype = "";
            contents = null;

            // FIXME: Ensure that we don't escape the sandbox here...

            if (!_contentTypeProvider.TryGetContentType(file, out mimetype))
            {
                return false;
            }

            var path = Path.Combine(@base, file);
            try
            {
                contents = File.ReadAllBytes(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        string GetTenantPortalPath()
        {
            var tenantPath = _executionContextManager.Current.Tenant.ToString();
            var portalPath = _portalManager.Current.Portal.ToString();
            return Path.Combine(_basePath, tenantPath, portalPath);
        }
    }
}