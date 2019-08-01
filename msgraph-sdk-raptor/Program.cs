using Microsoft.Extensions.Configuration;
using msgraph_sdk_raptor_compiler_lib.Templates;
using MsGraphSDKSnippetsCompiler;
using MsGraphSDKSnippetsCompiler.Models;
using System;
using System.Collections.Generic;

namespace MsGraphSDKRaptor
{
    public class Program
    {
        private static readonly IConfigurationRoot configuration = AppSettings.Config();
     
        static void Main(string[] args)
        { 
            CompileCSharpSnippets();
            Console.ReadKey();
        }

        private static void CompileCSharpSnippets()
        {
            //get the base template
            MSGraphSDKShellTemplate microsoftGraphShellTemplate = new MSGraphSDKShellTemplate();
            string microsoftGraphShellTemplateResult = microsoftGraphShellTemplate.TransformText();

            // get all files from the specified directory
            string targetDirectoryPath = configuration.GetSection("SnippetsDirectory").GetSection("CSharpPath").Value;

            SnippetsFileManager snippetsFileManager = new SnippetsFileManager();
            IEnumerable<string> snippetFiles = snippetsFileManager.GetAllSnippetsFilesFromDirectory(targetDirectoryPath);

            foreach (var markdownFile in snippetFiles)
            {
                string[] fileContent = snippetsFileManager.ReadMarkdownFile(markdownFile);
                string codeBlock = String.Empty;

                foreach (var snippet in fileContent)
                {
                    codeBlock += "\n" + snippet;
                }

                string codeToCompile = microsoftGraphShellTemplateResult
                           .Replace("//insert-code-here", codeBlock);
                Console.WriteLine(codeToCompile);

                //Compile Code
                MicrosoftGraphCSharpCompiler microsoftGraphCSharpCompiler = new MicrosoftGraphCSharpCompiler();
                CompilationResultsModel compilationResultsModel = microsoftGraphCSharpCompiler
                    .CompileSnippet(codeToCompile);

                if(!compilationResultsModel.IsSuccess)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var diagnostic in compilationResultsModel.Diagnostics)
                    {
                        Console.WriteLine(diagnostic.GetMessage());
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Compilation succeeded!");
                    Console.WriteLine("++++++++++++++++++++++\n");
                }
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}