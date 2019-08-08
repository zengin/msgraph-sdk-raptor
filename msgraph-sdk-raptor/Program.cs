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
        private static ISnippetsFileManager _snippetsFileManager = null;

        static void Main(string[] args)
        {
            MenuSelection();

            Console.ResetColor();
            Console.WriteLine("Press any key to exit app");
            Console.ReadKey();
        }

        private static void LogCompilationResults(CompilationCycleResultsModel logData)
        {
            //Log Compilation Results
            ICompilationResultsLogger compilationResultsLogger = new CompilationResultsTextFileLogger();
            compilationResultsLogger.Log(logData);

            /* Uncomment this section to use database logging
            string dbConnectioSettings = GetSettingsValue("ConnectionStrings", "Raptor");
            ICompilationResultsLogger compilationResultsLogger = new CompilationResultsSQLDatabaseLogger(dbConnectioSettings);
            compilationResultsLogger.Log(logData);
            */

            ShowConsoleMessage("Logging Complete!", ConsoleColor.Green);
        }

        #region Menu
        private static void MenuSelection()
        {
            string menuSelection;
            CompilationCycleResultsModel logData;

            do
            {
                menuSelection = DrawMenu();

                switch (menuSelection)
                {
                    case "1":
                        logData = CompileCSharpSnippets("CSharpPath-v1.0", Versions.V1);
                        LogCompilationResults(logData);
                        break;
                    case "2":
                        logData = CompileCSharpSnippets("CSharpPath-beta", Versions.Beta);
                        LogCompilationResults(logData);
                        break;
                    case "3":
                        menuSelection = "Exit";
                        break;
                    default:
                        Console.WriteLine("Invalid Selection");
                        break;
                }
                Console.ReadKey();
            } while (menuSelection != "Exit");
        }

        private static string DrawMenu()
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("|+- + - + - + - + - + -+|");
            Console.WriteLine("|+  Compile Snippets   +|");
            Console.WriteLine("|+- - - - - - - - - - -+|");
            Console.WriteLine("|+- CSharp            -+|");
            Console.WriteLine("|+-   1. v1.0         -+|");
            Console.WriteLine("|+-   2. beta         -+|");
            Console.WriteLine("|+- - - - - - - - - - -+|");
            Console.WriteLine("|+- + - + - + - + - + -+|");
            Console.WriteLine("Please select the menu option");
            string selection = Console.ReadLine();

            return selection;
        }
        #endregion

        private static CompilationCycleResultsModel CompileCSharpSnippets(string versionPath, Versions version)
        {
            //get the base csharp base template
            string microsoftGraphShellTemplateResult = GetBaseCSharpTemplate();

            // get all files from the specified directory
            string targetDirectoryPath = GetSettingsValue("SnippetsDirectory", versionPath);
            IEnumerable<string> snippetFiles = GetAllSnippetsFilesFromDirectory(targetDirectoryPath);

            List<CompilationResultsModel> compilationResultsModelsList = new List<CompilationResultsModel>();
            int totalCompiledSnippets = 0;
            int totalSnippetsWithError = 0;
            int totalErrors = 0;
            DateTime startCompilation = DateTime.Now;

            foreach (string markdownFile in snippetFiles)
            {
                string[] fileContent = ReadMarkdownFile(markdownFile);
                string codeToCompile = ConcatBaseTemplateWithSnippet(fileContent, microsoftGraphShellTemplateResult);
                Console.WriteLine(codeToCompile);

                //Compile Code
                IMicrosoftGraphSnippetsCompiler microsoftGraphCSharpCompiler = new MicrosoftGraphCSharpCompiler(markdownFile);
                CompilationResultsModel compilationResultsModel = microsoftGraphCSharpCompiler.CompileSnippet(codeToCompile, version);

                if (!compilationResultsModel.IsSuccess)
                {
                    totalSnippetsWithError += 1;
                    foreach (Diagnostic diagnostic in compilationResultsModel.Diagnostics)
                    {
                        ShowConsoleMessage($"{diagnostic.Id}\n {diagnostic.GetMessage()}", ConsoleColor.Red);
                        totalErrors += 1;
                    }
                }
                else
                {
                    ShowConsoleMessage("Compilation succeeded!", ConsoleColor.Yellow);
                }

                compilationResultsModel.Snippet = codeToCompile;
                //get the markdown filename only from the full path
                compilationResultsModel.MarkdownFileName = Path.GetFileName(markdownFile);
                compilationResultsModelsList.Add(compilationResultsModel);
                totalCompiledSnippets += 1;

                Console.ResetColor();
            }

            //Time taken to complete compiling the snippets in seconds
            decimal executionTimeTimeSpan = Convert.ToDecimal((DateTime.Now - startCompilation).TotalSeconds);

            CompilationCycleResultsModel compilationCycleResultsModel = new CompilationCycleResultsModel();
            compilationCycleResultsModel.compilationResultsModelList = compilationResultsModelsList;
            compilationCycleResultsModel.TotalCompiledSnippets = totalCompiledSnippets;
            compilationCycleResultsModel.TotalSnippetsWithError = totalSnippetsWithError;
            compilationCycleResultsModel.TotalErrors = totalErrors;
            compilationCycleResultsModel.Language = Languages.CSharp;
            compilationCycleResultsModel.Version = version;
            compilationCycleResultsModel.ExecutionTime = executionTimeTimeSpan;

            ShowConsoleMessage("Compilation Cycle Complete!", ConsoleColor.Green);
            Console.WriteLine($"Total Compiled Snippets: {compilationCycleResultsModel.TotalCompiledSnippets}");
            Console.WriteLine($"Total Snippets With Error:{compilationCycleResultsModel.TotalSnippetsWithError}");
            Console.WriteLine($"Total Errors: {compilationCycleResultsModel.TotalErrors}");
            Console.WriteLine($"Language: {compilationCycleResultsModel.Language}");
            Console.WriteLine($"Version: {compilationCycleResultsModel.Version}");
            Console.WriteLine($"Execution Time: {compilationCycleResultsModel.ExecutionTime} Secs");

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