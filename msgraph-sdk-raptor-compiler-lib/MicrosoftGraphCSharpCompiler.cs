using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using MsGraphSDKSnippetsCompiler.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MsGraphSDKSnippetsCompiler
{
    /// <summary>
    ///     Microsoft Graph SDK CSharp snippets compiler class
    /// </summary>
    public class MicrosoftGraphCSharpCompiler : IMicrosoftGraphSnippetsCompiler
    {
        public MicrosoftGraphCSharpCompiler()
        {

        }

        /// <summary>
        ///     Returns CompilationResultsModel which has the results status and the compilation diagnostics. 
        /// </summary>
        /// <param name="codeSnippet">The code snippet to be compiled.</param>
        /// <returns>CompilationResultsModel</returns>
        public CompilationResultsModel CompileSnippet(string codeSnippet)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeSnippet);
            string assemblyName = Path.GetRandomFileName();
            string assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            MetadataReference[] metadataReferences = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Private.CoreLib.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Console.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll"))
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
               assemblyName,
               syntaxTrees: new[] { syntaxTree },
               references: metadataReferences,
               options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            EmitResult emitResult = GetEmitResult(compilation);
            CompilationResultsModel results = GetCompilationResults(emitResult);

            return results;
        }

        /// <summary>
        ///     Gets the result of the Compilation.Emit method.
        /// </summary>
        /// <param name="compilation">Immutable respresentation of a single invocation of the compiler</param>
        private EmitResult GetEmitResult(CSharpCompilation compilation)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                EmitResult emitResult = compilation.Emit(memoryStream);
                return emitResult;
            }
        }

        /// <summary>
        ///     Checks whether the EmitResult is successfull and returns an instance of CompilationResultsModel. 
        /// </summary>
        /// <param name="emitResult">The result of the Compilation.Emit method.</param>
        private CompilationResultsModel GetCompilationResults(EmitResult emitResult)
        {
            CompilationResultsModel compilationResultsModel = new CompilationResultsModel();

            if (!emitResult.Success)
            {
                //We are only interested with warnings and errors hence the diagnostics filter
                IEnumerable<Diagnostic> failures = emitResult.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                compilationResultsModel.IsSuccess = true;
                compilationResultsModel.Diagnostics = failures;
            }
            else
            {
                compilationResultsModel.IsSuccess = false;
                compilationResultsModel.Diagnostics = null;
            }

            return compilationResultsModel;
        }
    }
}