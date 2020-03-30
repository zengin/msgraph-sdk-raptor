using MsGraphSDKSnippetsCompiler.Models;
using System;
using System.Collections.Generic;

namespace MsGraphSDKSnippetsCompiler.Services
{
    interface ICompileCycle
    {
        IEnumerable<CompileCycle> GetAll();
        CompileCycle Get(Guid id);
        CompileCycle Add(CompileCycle compileCycle);
        CompileCycle Update(CompileCycle compileCycle);
    }
}