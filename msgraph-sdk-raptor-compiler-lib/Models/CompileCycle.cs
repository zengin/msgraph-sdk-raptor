using System;
using System.ComponentModel.DataAnnotations;

namespace MsGraphSDKSnippetsCompiler.Models
{
    public class CompileCycle
    {
        [Key]
        public Guid CompileCycleID { get; set; }
        public int TotalCompiledSnippets { get; set; }
        public int TotalSnippetsWithError { get; set; }
        public int TotalErrors { get; set; }
        public Languages Language { get; set; }
        public decimal ExecutionTime { get; set; }
        public DateTime CompileDate { get; set; }
    }

    public enum Languages
    {
        CSharp,
        JavaScript,
        Java,
        ObjC
    }
}