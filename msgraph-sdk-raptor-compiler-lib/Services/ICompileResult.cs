using MsGraphSDKSnippetsCompiler.Models;
using System;
using System.Collections.Generic;

namespace MsGraphSDKSnippetsCompiler.Services
{
    interface ICompileResult
    {
        IEnumerable<CompileResult> GetAll();
        CompileResult Get(Guid id);
        CompileResult Add(CompileResult compileResult);
        CompileResult Update(CompileResult compileResult);
    }
}