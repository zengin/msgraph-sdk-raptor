using System.Collections.Generic;

namespace MsGraphSDKSnippetsCompiler
{
    interface ISnippetsFileManager
    {
        string[] ReadMarkdownFile(string fileName);
        IEnumerable<string> GetAllSnippetsFilesFromDirectory(string directoryFile);
    }
}