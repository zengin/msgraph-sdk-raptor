using MsGraphSDKSnippetsCompiler.Models;

namespace MsGraphSDKSnippetsCompiler
{
    public interface ICompilationResultsLogger
    {
        void Log(CompilationCycleResultsModel compilationCycleResultsModel);
    }
}