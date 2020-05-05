// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using MsGraphSDKSnippetsCompiler.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestsCommon
{
    public static class KnownIssues
    {
        /// <summary>
        /// Known issue message for cases where there is a feature missing in SDK
        /// </summary>
        private const string NotSupported = "Feature not supported by SDK";

        /// <summary>
        /// Gets known issues
        /// </summary>
        /// <returns>A mapping of test names into known issues</returns>
        public static Dictionary<string, string> GetIssues()
        {
            return new Dictionary<string, string>()
            {
                { "get-rangeformat-csharp-V1-compiles", NotSupported }
            };
        }
    }

    /// <summary>
    /// Generates TestCaseData for NUnit
    /// </summary>
    public static class TestDataGenerator
    {
        /// <summary>
        /// Snippet links as shown in markdown files in docs repo
        /// </summary>
        private const string SnippetLinkPattern = @"includes\/snippets\/csharp\/(.*)\-csharp\-snippets\.md";

        /// <summary>
        /// Regex matching the pattern above
        /// </summary>
        private static readonly Regex SnippetLinkRegex = new Regex(SnippetLinkPattern, RegexOptions.Compiled);

        /// <summary>
        /// Generates a dictionary mapping from snippet file name to documentation page listing the snippet.
        /// </summary>
        /// <param name="version">Docs version (e.g. V1, Beta)</param>
        /// <returns>Dictionary holding the mapping from snippet file name to documentation page listing the snippet.</returns>
        private static Dictionary<string, string> GetDocumentationLinks(Versions version)
        {
            var documentationLinks = new Dictionary<string, string>();
            var documentationDirectory = GraphDocsDirectory.GetDocumentationDirectory(version);
            var files = Directory.GetFiles(documentationDirectory);
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var fileName = Path.GetFileNameWithoutExtension(file);
                var docsLink = $"https://docs.microsoft.com/en-us/graph/api/{fileName}?view=graph-rest-{new VersionString(version)}&tabs=csharp";

                var match = SnippetLinkRegex.Match(content);
                while (match.Success)
                {
                    documentationLinks[match.Groups[1].Value + "-csharp-snippets.md"] = docsLink;
                    match = match.NextMatch();
                }
            }

            return documentationLinks;
        }

        /// <summary>
        /// For each snippet file creates a test case which takes the file name and version as reference
        /// Test case name is also set to to unique name based on file name
        /// </summary>
        /// <param name="version">Docs version (e.g. V1, Beta)</param>
        /// <returns>
        /// TestCaseData to be consumed by C# compilation tests
        /// </returns>
        public static IEnumerable<TestCaseData> GetTestCaseData(Versions version)
        {
            var documentationLinks = GetDocumentationLinks(version);
            var knownIssues = KnownIssues.GetIssues();
            var snippetFileNames = documentationLinks.Keys.ToList();
            return from fileName in snippetFileNames                                // e.g. application-addpassword-csharp-snippets.md
                   let testNamePostfix = version.ToString() + "-compiles"           // e.g. Beta-compiles
                   let testName = fileName.Replace("snippets.md", testNamePostfix)  // e.g. application-addpassword-csharp-Beta-compiles
                   let docsLink = documentationLinks[fileName]
                   let isKnownIssue = knownIssues.ContainsKey(testName)
                   let knownIssueMessage = isKnownIssue ? knownIssues[testName] : string.Empty
                   let testCaseData = new CsharpTestData
                   {
                       Version = version,
                       IsKnownIssue = isKnownIssue,
                       KnownIssueMessage = knownIssueMessage,
                       DocsLink = docsLink,
                       FileName = fileName
                   }
                   select new TestCaseData(testCaseData).SetName(testName);
        }
    }
}
