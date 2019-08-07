using System;
using System.IO;
using Microsoft.CodeAnalysis;
using MsGraphSDKSnippetsCompiler.Models;

namespace MsGraphSDKSnippetsCompiler
{
    public class CompilationResultsTextFileLogger : ICompilationResultsLogger
    {
        public CompilationResultsTextFileLogger()
        {

        }

        /// <summary>
        /// Log the compilation data in a text file 
        /// </summary>
        /// <param name="compilationCycleResultsModel">Model with all the necessary data after a compilation cycle</param>
        public void Log(CompilationCycleResultsModel compilationCycleResultsModel)
        {
            string logPath = "CompileLog.md";
            if (File.Exists(logPath))
            {
                // Create a log file to write on
                using (StreamWriter streamWriter = File.CreateText(logPath))
                {
                    //Log Compile Cycle
                    streamWriter.WriteLine("|++++++++++++++++++++++++|");
                    streamWriter.Write($"#### Total CompiledSnippets:  {compilationCycleResultsModel.TotalCompiledSnippets}");
                    streamWriter.Write($"\r Total Snippets With Errors: {compilationCycleResultsModel.TotalSnippetsWithError}");
                    streamWriter.Write($"\r Total Errors: {compilationCycleResultsModel.TotalErrors}");
                    streamWriter.Write($"\r Language: {compilationCycleResultsModel.Language}");
                    streamWriter.Write($"\r Execution Time: {compilationCycleResultsModel.ExecutionTime} secs");
                    streamWriter.Write($"\r Compilation Date: {DateTime.Now}");
                    streamWriter.WriteLine("|++++++++++++++++++++++++|\n");

                    //Log CompileCycle Results in txt
                    foreach (CompilationResultsModel compilationResultsModel in compilationCycleResultsModel.compilationResultsModelList)
                    {
                        streamWriter.WriteLine($"#### Markdown FileName: {compilationResultsModel.MarkdownFileName}");
                        streamWriter.WriteLine($"#### IsSuccess: {compilationResultsModel.IsSuccess}");
                        streamWriter.WriteLine($"``` \n {compilationResultsModel.Snippet} \n ```");
                        streamWriter.WriteLine("|++++++++++++++++++++++++|");

                        if (compilationResultsModel.Diagnostics != null)
                        {
                            foreach (Diagnostic diagnostics in compilationResultsModel.Diagnostics)
                            {
                                streamWriter.WriteLine($"\n\t#### Error Code: {diagnostics.Id}");
                                streamWriter.WriteLine($"\t#### IsWarningAsError {diagnostics.IsWarningAsError}");
                                streamWriter.WriteLine($"\t#### WarningLevel {diagnostics.WarningLevel}");
                                streamWriter.WriteLine($"\t#### Error Message {diagnostics.GetMessage()}\n");
                            }
                        }
                    }
                }
            }
        }
    }
}