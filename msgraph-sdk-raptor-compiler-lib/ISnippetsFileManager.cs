using System.Collections.Generic;

namespace MsGraphSDKSnippetsCompiler
{
    public interface ISnippetsFileManager
    {
        string[] ReadMarkdownFile(string fileName);
        IEnumerable<string> GetAllSnippetsFilesFromDirectory(string directoryFile);
    }
}