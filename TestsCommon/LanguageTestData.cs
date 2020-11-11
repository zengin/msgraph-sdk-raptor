using MsGraphSDKSnippetsCompiler.Models;

namespace TestsCommon
{
    public class LanguageTestData
    {
        /// <summary>
        /// Docs version e.g. V1 or Beta
        /// </summary>
        public Versions Version { get; set; }

        /// <summary>
        /// Whether the test case is failing due to a known issue
        /// </summary>
        public bool IsKnownIssue { get; set; }

        /// <summary>
        /// Message to represent known issue
        /// </summary>
        public string KnownIssueMessage { get; set; }

        /// <summary>
        /// Documentation link where snippet is shown
        /// </summary>
        public string DocsLink { get; set; }

        /// <summary>
        /// Snippet file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Optional dll path to load Microsoft.Graph from a local resource instead of published nuget
        /// </summary>
        public string DllPath { get; set; }
        /// <summary>
        /// Optional. Version to use for the java core library. Ignored when using JavaPreviewLibPath
        /// </summary>
        public string JavaCoreVersion { get; set; }
        /// <summary>
        /// Optional. Version to use for the java service library. Ignored when using JavaPreviewLibPath
        /// </summary>
        public string JavaLibVersion { get; set; }
        /// <summary>
        /// Optional. Folder container the java core and java service library repositories so the unit testing uses that local version instead.
        /// </summary>
        public string JavaPreviewLibPath { get; set; }
    }
}
