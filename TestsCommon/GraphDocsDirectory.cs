// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using MsGraphSDKSnippetsCompiler.Models;
using System;
using System.IO;

namespace TestsCommon
{
    /// <summary>
    /// Init-once access to snippets directory
    /// </summary>
    public static class GraphDocsDirectory
    {
        /// <summary>
        /// Represents where the snippets are stored. Expected to refer to a single directory for each assembly.
        /// </summary>
        private static string SnippetsDirectory = null;

        /// <summary>
        /// Represents the root directory where you checkout microsoft-graph-docs repo
        /// </summary>
        const string RootGitDirectory = @"C:\github";

        /// <summary>
        /// Sets snippets directory only once and refers to the string if it is already set
        /// Assumes that default "git clone <remote-reference>" command is used, in other words,
        /// the repo is always in microsoft-graph-docs folder under RootDirectory defined above
        /// </summary>
        /// <param name="version">Docs version (e.g. V1 or Beta)</param>
        /// <returns>
        /// C# snippets directory
        /// </returns>
        public static string GetCsharpSnippetsDirectory(Versions version)
        {
            if (SnippetsDirectory is object)
            {
                return SnippetsDirectory;
            }

            var msGraphDocsRepoLocation = Environment.GetEnvironmentVariable("BUILD_SOURCESDIRECTORY") ?? RootGitDirectory;
            SnippetsDirectory = Path.Join(msGraphDocsRepoLocation, $@"microsoft-graph-docs\api-reference\{new VersionString(version)}\includes\snippets\csharp");

            return SnippetsDirectory;
        }

        /// <summary>
        /// Gets directory holding Microsoft Graph documentation in markdown format
        /// </summary>
        /// <param name="version">Docs version (e.g. V1 or Beta)</param>
        /// <returns>
        /// Directory holding Microsoft Graph documentation in markdown format
        /// </returns>
        public static string GetDocumentationDirectory(Versions version)
        {
            var msGraphDocsRepoLocation = Environment.GetEnvironmentVariable("BUILD_SOURCESDIRECTORY") ?? RootGitDirectory;
            return Path.Join(msGraphDocsRepoLocation, $@"microsoft-graph-docs\api-reference\{new VersionString(version)}\api");
        }
    }
}
