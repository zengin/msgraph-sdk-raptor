using MsGraphSDKSnippetsCompiler.Models;

namespace MsGraphSDKSnippetsCompiler
{
    interface ICompilationResultsLogger
    {
        void Log(CompilationCycleResultsModel compilationResultsModelList);
    }
}