using System;
using System.ComponentModel.DataAnnotations;

namespace MsGraphSDKSnippetsCompiler.Models
{
    public class CompileResultsError
    {
        [Key]
        public Guid CompileResultsErrorID { get; set; }
        public Guid CompileResultsID { get; set; }
        public string ErrorCode { get; set; }
        public bool IsWarning { get; set; }
        public int WarningLevel { get; set; }
        public string ErrorMessage { get; set; }
    }
}