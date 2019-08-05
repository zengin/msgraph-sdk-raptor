using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using MsGraphSDKSnippetsCompiler.Models;
using MsGraphSDKSnippetsCompiler.Templates;
using MsGraphSDKSnippetsCompiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace MsGraphSDKRaptor
{
    public class Program
    {
        private static readonly IConfigurationRoot _configuration = AppSettings.Config();
        private static SnippetsFileManager _snippetsFileManager = null;

        static void Main(string[] args)
        {
            CompilationCycleResultsModel logData = CompileCSharpSnippets();

            //Log Compilation Results
            /* Uncomment this block to log to database
            string dbConnectioSettings = GetSettingsValue("ConnectionStrings", "Raptor");
            CompilationResultsLogger compilationResultsLogger = new CompilationResultsLogger(dbConnectioSettings);
            compilationResultsLogger.Log(logData);
            */
            Console.ReadKey();
        }

        private static CompilationCycleResultsModel CompileCSharpSnippets()
        {
            //get the base csharp base template
            string microsoftGraphShellTemplateResult = GetBaseCSharpTemplate();

            // get all files from the specified directory
            string targetDirectoryPath = GetSettingsValue("SnippetsDirectory", "CSharpPath");
            IEnumerable<string> snippetFiles = GetAllSnippetsFilesFromDirectory(targetDirectoryPath);

            List<CompilationResultsModel> compilationResultsModelsList = new List<CompilationResultsModel>();
            int totalCompiledSnippets = 0;
            int totalErrors = 0;
            DateTime startCompilation = DateTime.Now;

            foreach (string markdownFile in snippetFiles)
            {
                string[] fileContent = ReadMarkdownFile(markdownFile);

                string codeToCompile = ConcatBaseTemplateWithSnippet(fileContent, microsoftGraphShellTemplateResult);
                Console.WriteLine(codeToCompile);

                //Compile Code
                MicrosoftGraphCSharpCompiler microsoftGraphCSharpCompiler = new MicrosoftGraphCSharpCompiler(markdownFile);
                CompilationResultsModel compilationResultsModel = microsoftGraphCSharpCompiler
                    .CompileSnippet(codeToCompile);

                if (!compilationResultsModel.IsSuccess)
                {
                    foreach (Diagnostic diagnostic in compilationResultsModel.Diagnostics)
                    {
                        ShowConsoleMessage($"{diagnostic.GetMessage()} - {diagnostic.Id}" , ConsoleColor.Red);
                        totalErrors += 1;
                    }
                }
                else
                {
                    ShowConsoleMessage("Compilation succeeded!", ConsoleColor.Yellow);
                }

                compilationResultsModel.Snippet = codeToCompile;
                compilationResultsModel.MarkdownFileName = Path.GetFileName(markdownFile);
                compilationResultsModelsList.Add(compilationResultsModel);
                totalCompiledSnippets += 1;

                Console.ResetColor();
            }

            //Time taken to complete compiling the snippets
            decimal executionTimeTimeSpan = Convert.ToDecimal((DateTime.Now - startCompilation).TotalSeconds);

            CompilationCycleResultsModel compilationCycleResultsModel = new CompilationCycleResultsModel();
            compilationCycleResultsModel.compilationResultsModelList = compilationResultsModelsList;
            compilationCycleResultsModel.TotalCompiledSnippets = totalCompiledSnippets;
            compilationCycleResultsModel.TotalErrors = totalErrors;
            compilationCycleResultsModel.Language = Languages.CSharp;
            compilationCycleResultsModel.ExecutionTime = executionTimeTimeSpan;
         
            return compilationCycleResultsModel;
        }

        #region SharedMethods
        private static string GetBaseCSharpTemplate()
        {
            //get the base template
            MSGraphSDKShellTemplate microsoftGraphShellTemplate = new MSGraphSDKShellTemplate();
            string microsoftGraphShellTemplateResult = microsoftGraphShellTemplate.TransformText();

            return microsoftGraphShellTemplateResult;
        }

        private static string GetSettingsValue(string section, string key)
        {
            string targetDirectoryPath = _configuration.GetSection(section).GetSection(key).Value;
            return targetDirectoryPath;
        }

        private static IEnumerable<string> GetAllSnippetsFilesFromDirectory(string targetDirectoryPath)
        {
            _snippetsFileManager = new SnippetsFileManager();
            IEnumerable<string> snippetFiles = _snippetsFileManager.GetAllSnippetsFilesFromDirectory(targetDirectoryPath);

            return snippetFiles;
        }

        private static string[] ReadMarkdownFile(string markdownFile)
        {
            string[] fileContent = _snippetsFileManager.ReadMarkdownFile(markdownFile);
            return fileContent;
        }

        private static string ConcatBaseTemplateWithSnippet(string[] fileContent, string microsoftGraphShellTemplateResult)
        {
            string codeBlock = String.Empty;
            foreach (string snippet in fileContent)
            {
                codeBlock += "\n" + snippet;
            }

            string codeToCompile = microsoftGraphShellTemplateResult
                       .Replace("//insert-code-here", codeBlock);

            return codeToCompile;
        }

        private static void ShowConsoleMessage(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.WriteLine("++++++++++++++++++++++\n");
        }
        #endregion
    }
}