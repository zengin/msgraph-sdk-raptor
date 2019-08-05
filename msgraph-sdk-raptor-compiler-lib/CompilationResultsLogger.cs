using System;
using Microsoft.CodeAnalysis;
using MsGraphSDKSnippetsCompiler.Data;
using MsGraphSDKSnippetsCompiler.Models;
using MsGraphSDKSnippetsCompiler.Services;

namespace MsGraphSDKSnippetsCompiler
{
    public class CompilationResultsLogger : ICompilationResultsLogger
    {
        private readonly string _connectionString;

        public CompilationResultsLogger(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Logd the compilation data in a database 
        /// </summary>
        /// <param name="compilationCycleResultsModel">Model with all the necessary data afer a compilation cycle</param>
        public void Log(CompilationCycleResultsModel compilationCycleResultsModel)
        {
            //Log Compile Cycle
            CompileCycle compileCycle = new CompileCycle();
            compileCycle.CompileCycleID = Guid.NewGuid();
            compileCycle.TotalCompiledSnippets = compilationCycleResultsModel.TotalCompiledSnippets;
            compileCycle.TotalErrors = compilationCycleResultsModel.TotalErrors;
            compileCycle.Language = compilationCycleResultsModel.Language;
            compileCycle.ExecutionTime = compilationCycleResultsModel.ExecutionTime;
            compileCycle.CompileDate = DateTime.Now;

            ICompileCycle compileCycleData = new CompileCycleData(new RaptorDbContext(_connectionString));
            compileCycleData.Add(compileCycle);

            //Log CompileCycle Results in DB
            foreach (CompilationResultsModel compilationResultsModel in compilationCycleResultsModel.compilationResultsModelList)
            {
                CompileResult compileResult = new CompileResult();
                compileResult.CompileResultsID = Guid.NewGuid();
                compileResult.CompileCycleID = compileCycle.CompileCycleID;
                compileResult.IsSuccess = compilationResultsModel.IsSuccess;
                compileResult.FileName = compilationResultsModel.MarkdownFileName;
                compileResult.Snippet = compilationResultsModel.Snippet;

                ICompileResult compileResultData = new CompileResultData(new RaptorDbContext(_connectionString));
                compileResultData.Add(compileResult);

                if (compilationResultsModel.Diagnostics != null)
                {
                    foreach (Diagnostic diagnostics in compilationResultsModel.Diagnostics)
                    {
                        CompileResultsError compileResultsError = new CompileResultsError();
                        compileResultsError.CompileResultsErrorID = Guid.NewGuid();
                        compileResultsError.CompileResultsID = compileResult.CompileResultsID;
                        compileResultsError.ErrorMessage = diagnostics.GetMessage();

                        ICompileResultsError compileResultsErrorData = new CompileResultsErrorData(new RaptorDbContext(_connectionString));
                        compileResultsErrorData.Add(compileResultsError);
                    }
                }
            }           
        }
    }
}