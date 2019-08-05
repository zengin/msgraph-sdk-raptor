using MsGraphSDKSnippetsCompiler.Models;
using System;
using System.Collections.Generic;

namespace MsGraphSDKSnippetsCompiler.Services
{
    interface ICompileResultsError
    {
        IEnumerable<CompileResultsError> GetAll();
        CompileResultsError Get(Guid id);
        CompileResultsError Add(CompileResultsError compileResultsError);
        CompileResultsError Update(CompileResultsError compileResultsError);
    }
}