using System;
using System.ComponentModel.DataAnnotations;

namespace MsGraphSDKSnippetsCompiler.Models
{
    public class CompileResultsView
    {
        [Key]
        public Guid CompileResultsID { get; set; }
        public Guid CompileCycleID { get; set; }
        public Languages Language { get; set; }
        public bool IsSuccess { get; set; }
        public string FileName { get; set; }
        public Guid CompileResultsErrorID { get; set; }
        public string ErrorMessage { get; set; }       
        public string Snippet { get; set; }
        public DateTime CompileDate { get; set; }
    }
}