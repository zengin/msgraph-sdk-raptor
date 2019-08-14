using System;
using System.ComponentModel.DataAnnotations;

namespace MsGraphSDKSnippetsCompiler.Models
{
    public class CompileResult
    {
        [Key]
        public Guid CompileResultsID { get; set; }
        public Guid CompileCycleID { get; set; }
        public bool IsSuccess { get; set; }
        public string FileName { get; set; }
        public string Snippet { get; set; }
    }
}