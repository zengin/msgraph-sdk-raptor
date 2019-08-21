using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using MsGraphSDKSnippetsCompiler.Models;
using Newtonsoft.Json;

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
                    streamWriter.WriteLine($"#### Execution Time: {compilationCycleResultsModel.ExecutionTime} secs");
                    streamWriter.WriteLine($"#### Compilation Date: {DateTime.Now}");
                    streamWriter.WriteLine("|++++++++++++++++++++++++|\n");

                    streamWriter.WriteLine("Total CompiledSnippets| Total Snippets With Errors| Total Errors| Language| Version|");
                    streamWriter.WriteLine("|--|--|--|--|--|");
                    streamWriter.WriteLine($"{compilationCycleResultsModel.TotalCompiledSnippets} | {compilationCycleResultsModel.TotalSnippetsWithError} " +
                                $"| {compilationCycleResultsModel.TotalErrors} | {compilationCycleResultsModel.Language} | {compilationCycleResultsModel.Version}");
                    streamWriter.WriteLine("\n\n");

                    IEnumerable<ErrorReferenceDictionaryStats> result = GetCompilationCycleStatus(compilationCycleResultsModel.CompilationCycleDiagnostics);

                    if (result != null)
                    {
                        streamWriter.WriteLine("Error Count| Error Id | Error Description |");
                        streamWriter.WriteLine("|--|--|--|");

                        foreach (ErrorReferenceDictionaryStats errorStat in result)
                        {
                            streamWriter.WriteLine($"{errorStat.Count} | {errorStat.Id} | {errorStat.Error} |");
                        }
                    }

                    //Log CompileCycle Results in Markdown file
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

        public IEnumerable<ErrorReferenceDictionaryStats> GetCompilationCycleStatus(List<Diagnostic> allErrorDiagnostics)
        {
            //log error categories
            List<ErrorGroup> errorGroupResults = (from d in allErrorDiagnostics
                                                  group d by d.Id into g
                                                  where g.Count() > 1
                                                  select new ErrorGroup { Key = g.Key, Count = g.Count() }).ToList();

            List<ErrorReferenceDictionary> errorReferenceDictionary = GetErrorReferenceDictionary();

            IEnumerable<ErrorReferenceDictionaryStats> result = (from d in errorGroupResults
                                                                 join s in errorReferenceDictionary on d.Key equals s.Id
                                                                 select new ErrorReferenceDictionaryStats { Id = s.Id, Error = s.Error, Description = s.Description, Count = d.Count });

            return result;
        }

        private static List<ErrorReferenceDictionary> GetErrorReferenceDictionary()
        {
            using (StreamReader streamReader = new StreamReader("ErrorCodes/CSharp.json"))
            {
                string json = @streamReader.ReadToEnd().ToString();
                List<ErrorReferenceDictionary> errorReferenceDictionary = JsonConvert.DeserializeObject<List<ErrorReferenceDictionary>>(json);

                return errorReferenceDictionary;
            }
        }
    }
}