// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using MsGraphSDKSnippetsCompiler.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestsCommon
{
    public static class KnownIssues
    {
        /// <summary>
        /// Known issue message for cases where composable functions feature is missing in SDK
        /// </summary>
        private const string FeatureNotSupported = "Range composable functions are not supported by SDK\r\n"
            + "https://github.com/microsoftgraph/msgraph-sdk-dotnet/issues/490";

        /// <summary>
        /// Known issue message for cases where HTTP snippet input should be fixed
        /// </summary>
        private const string HttpSnippetWrong = "Http snippet should be fixed";

        /// <summary>
        /// Constructs property not found message
        /// </summary>
        /// <param name="type">Type that need to define the property</param>
        /// <param name="property">Property that needs to be defined but missing in metadata</param>
        /// <returns>String representation of property not found message</returns>
        private static string GetPropertyNotFoundMessage(string type, string property)
        {
            return HttpSnippetWrong + string.Format(CultureInfo.InvariantCulture, ": {0} does not contain definition of {1} in metadata", type, property);
        }

        /// <summary>
        /// Gets known issues
        /// </summary>
        /// <returns>A mapping of test names into known issues</returns>
        public static Dictionary<string, string> GetIssues()
        {
            return new Dictionary<string, string>()
            {
                { "call-transfer-csharp-V1-compiles", GetPropertyNotFoundMessage("InvitationParticipantInfo", "EndpointType") },
                { "create-educationschool-from-educationroot-csharp-V1-compiles", GetPropertyNotFoundMessage("EducationSchool", "Status") },
                { "create-item-attachment-from-eventmessage-csharp-V1-compiles", HttpSnippetWrong + ": Item needs to be an OutlookItem object, not a string" },
                { "create-rangeborder-from-rangeformat-csharp-V1-compiles", FeatureNotSupported },
                { "create-reference-attachment-with-post-csharp-V1-compiles", GetPropertyNotFoundMessage("ReferenceAttachment", "SourceUrl, ProviderType, Permission and IsFolder") },
                { "create-tablecolumn-from-table-csharp-V1-compiles", HttpSnippetWrong + "Id should be string not int" },
                { "get-borders-csharp-V1-compiles", FeatureNotSupported },
                { "get-formatprotection-csharp-V1-compiles", FeatureNotSupported },
                { "get-rangeborder-csharp-V1-compiles", FeatureNotSupported },
                { "get-rangebordercollection-csharp-V1-compiles", FeatureNotSupported },
                { "get-rangefill-csharp-V1-compiles", FeatureNotSupported },
                { "get-rangefont-csharp-V1-compiles", FeatureNotSupported },
                { "get-rangeformat-csharp-V1-compiles", FeatureNotSupported },
                { "post-reply-csharp-V1-compiles", HttpSnippetWrong + ": Odata.Type for concreate Attachment type should be added" },
                { "rangefill-clear-csharp-V1-compiles", FeatureNotSupported },
                { "rangeformat-autofitcolumns-csharp-V1-compiles", FeatureNotSupported },
                { "rangeformat-autofitrows-csharp-V1-compiles", FeatureNotSupported },
                { "update-activitybasedtimeoutpolicy-csharp-V1-compiles", GetPropertyNotFoundMessage("ActivityBasedTimeoutPolicy", "Type")},
                { "update-formatprotection-csharp-V1-compiles", FeatureNotSupported },
                { "update-homerealmdiscoverypolicy-csharp-V1-compiles", GetPropertyNotFoundMessage("HomeRealmDiscoveryPolicy", "Type") },
                { "update-rangeborder-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangefill-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangefont-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-fill-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-fill-three-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-fill-two-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-font-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-font-two-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-three-csharp-V1-compiles", FeatureNotSupported },
                { "update-rangeformat-two-csharp-V1-compiles", FeatureNotSupported },
                { "update-tokenissuancepolicy-csharp-V1-compiles", GetPropertyNotFoundMessage("TokenIssuancePolicy", "Type")},
                { "update-tokenlifetimepolicy-csharp-V1-compiles", GetPropertyNotFoundMessage("TokenLifetimePolicy", "Type") },
                {" range-cell-csharp-V1-compiles", FeatureNotSupported },
                {" range-clear-csharp-V1-compiles", FeatureNotSupported },
                {" range-column-csharp-V1-compiles", FeatureNotSupported },
                {" range-delete-csharp-Beta-compiles", FeatureNotSupported },
                {" range-delete-csharp-V1-compiles", FeatureNotSupported },
                {" range-usedrange-valuesonly-csharp-V1-compiles", FeatureNotSupported },
                {" update-rangeformat-font-three-csharp-V1-compiles", FeatureNotSupported },
                {" workbookrange-columnsafter-csharp-Beta-compiles", FeatureNotSupported },
                {" workbookrange-columnsafter-csharp-V1-compiles", FeatureNotSupported },
                {" workbookrange-columnsbefore-csharp-Beta-compiles", FeatureNotSupported },
                {" workbookrange-columnsbefore-csharp-V1-compiles", FeatureNotSupported },
                {"create-rangeborder-from-rangeformat-csharp-Beta-compiles", FeatureNotSupported },
                {"get-borders-csharp-Beta-compiles", FeatureNotSupported },
                {"get-formatprotection-csharp-Beta-compiles", FeatureNotSupported },
                {"get-rangeborder-csharp-Beta-compiles", FeatureNotSupported },
                {"get-rangebordercollection-csharp-Beta-compiles", FeatureNotSupported },
                {"get-rangefill-csharp-Beta-compiles", FeatureNotSupported },
                {"get-rangefont-csharp-Beta-compiles", FeatureNotSupported },
                {"get-rangeformat-csharp-Beta-compiles", FeatureNotSupported },
                {"get-rows-csharp-Beta-compiles", FeatureNotSupported },
                {"get-rows-csharp-V1-compiles", FeatureNotSupported },
                {"range-clear-csharp-Beta-compiles", FeatureNotSupported },
                {"range-entirecolumn-csharp-Beta-compiles", FeatureNotSupported },
                {"range-entirecolumn-csharp-V1-compiles", FeatureNotSupported },
                {"range-entirerow-csharp-Beta-compiles", FeatureNotSupported },
                {"range-entirerow-csharp-V1-compiles", FeatureNotSupported },
                {"range-insert-csharp-Beta-compiles", FeatureNotSupported },
                {"range-insert-csharp-V1-compiles", FeatureNotSupported },
                {"range-lastcell-csharp-Beta-compiles", FeatureNotSupported },
                {"range-lastcell-csharp-V1-compiles", FeatureNotSupported },
                {"range-lastcolumn-csharp-Beta-compiles", FeatureNotSupported },
                {"range-lastcolumn-csharp-V1-compiles", FeatureNotSupported },
                {"range-lastrow-csharp-Beta-compiles", FeatureNotSupported },
                {"range-lastrow-csharp-V1-compiles", FeatureNotSupported },
                {"range-merge-csharp-Beta-compiles", FeatureNotSupported },
                {"range-merge-csharp-V1-compiles", FeatureNotSupported },
                {"range-unmerge-csharp-Beta-compiles", FeatureNotSupported },
                {"range-unmerge-csharp-V1-compiles", FeatureNotSupported },
                {"range-usedrange-csharp-Beta-compiles", FeatureNotSupported },
                {"range-usedrange-csharp-V1-compiles", FeatureNotSupported },
                {"range-usedrange-valuesonly-csharp-V1-compiles", FeatureNotSupported },
                {"rangefill-clear-csharp-Beta-compiles", FeatureNotSupported },
                {"rangeformat-autofitcolumns-csharp-Beta-compiles", FeatureNotSupported },
                {"rangeformat-autofitrows-csharp-Beta-compiles", FeatureNotSupported },
                {"rangesort-apply-csharp-Beta-compiles", FeatureNotSupported },
                {"rangesort-apply-csharp-V1-compiles", FeatureNotSupported },
                {"update-formatprotection-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeborder-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangefill-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangefont-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-fill-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-fill-three-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-fill-two-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-font-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-font-three-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-font-two-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-three-csharp-Beta-compiles", FeatureNotSupported },
                {"update-rangeformat-two-csharp-Beta-compiles", FeatureNotSupported },
                {"workbookrange-rowsabove-csharp-Beta-compiles", FeatureNotSupported },
                {"workbookrange-rowsabove-csharp-V1-compiles", FeatureNotSupported },
                {"workbookrange-rowsabove-nocount-csharp-V1-compiles", FeatureNotSupported },
                {"workbookrange-rowsbelow-csharp-Beta-compiles", FeatureNotSupported },
                {"workbookrange-rowsbelow-csharp-V1-compiles", FeatureNotSupported },
                {"workbookrange-rowsbelow-nocount-csharp-V1-compiles", FeatureNotSupported },
                {"workbookrange-visibleview-csharp-Beta-compiles", FeatureNotSupported },
                {"workbookrange-visibleview-csharp-V1-compiles", FeatureNotSupported },
                {"workbookrangeview-range-csharp-Beta-compiles", FeatureNotSupported },
                {"workbookrangeview-range-csharp-V1-compiles", FeatureNotSupported },
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
        /// <param name="knownFailuresRequested">return whether known failures as test cases or not</param>
        /// <returns>
        /// TestCaseData to be consumed by C# compilation tests
        /// </returns>
        public static IEnumerable<TestCaseData> GetTestCaseData(Versions version, bool knownFailuresRequested = false)
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
                   where !(isKnownIssue ^ knownFailuresRequested) // select known issues if requested
                   select new TestCaseData(testCaseData).SetName(testName);
        }
    }
}
