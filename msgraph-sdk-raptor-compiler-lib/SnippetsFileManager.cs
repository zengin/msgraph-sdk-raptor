using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MsGraphSDKSnippetsCompiler
{
    public class SnippetsFileManager : ISnippetsFileManager
    {
        /// <summary>
        ///     Get all snippets full filenames from a directory
        /// </summary>
        /// <param name="targetDirectory">The target directory path</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> GetAllSnippetsFilesFromDirectory(string targetDirectoryPath)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectoryPath);
            return fileEntries;
        }

        /// <summary>
        ///     Reads all the contents of a Markdown file
        /// </summary>
        /// <param name="markdownFile"></param>
        /// <returns>string[]</returns>
        public string[] ReadMarkdownFile(string markdownFile)
        {
            if (File.Exists(markdownFile))
            {
                string[] fileContent = File.ReadAllLines(markdownFile);
                fileContent = ExtractCodeFromMarkDownContent(fileContent);
                return fileContent;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        ///     Gets the code section only within the markdown file
        /// </summary>
        /// <param name="markdownFile"></param>
        /// <returns></returns>
        private string[] ExtractCodeFromMarkDownContent(string[] markdownFile)
        {
            string description = "description: \"Automatically generated file. DO NOT MODIFY\"";

            //instructs the regular expression engine to interpret the double quotes characters literally rather than as metacharacters
            description = Regex.Escape(description);
            string pattern = $@"(---\\? |```\\? |csharp\\? |{description}\\? )";

            List<string> result = new List<string>();
            string newLine;

            foreach (var line in markdownFile)
            {
                if (line != string.Empty)
                {
                    newLine = Regex.Replace(line, pattern, string.Empty, RegexOptions.IgnorePatternWhitespace);
                    if (newLine != string.Empty)
                    {
                        result.Add(newLine);
                    }
                }
            }
            return result.ToArray();
        }
    }
}