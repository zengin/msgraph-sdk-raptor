using Microsoft.CodeAnalysis;
using MsGraphSDKSnippetsCompiler.Models;
using System.Collections.Generic;

namespace MsGraphSDKSnippetsCompiler
{
    public interface ICompilationResultsLogger
    {
        void Log(CompilationCycleResultsModel compilationCycleResultsModel);
        //this may introduce a dependancy on roslyn will require refactoring
        IEnumerable<ErrorReferenceDictionaryStats> GetCompilationCycleStatus(List<Diagnostic> allErrorDiagnostics);
    }
}