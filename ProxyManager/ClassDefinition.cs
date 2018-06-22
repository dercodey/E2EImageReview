using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Linq;

namespace ProxyManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ClassDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="forNamespace"></param>
        /// <param name="className"></param>
        /// <param name="baseNames"></param>
        /// <param name="methods"></param>
        public ClassDefinition(string forNamespace, string className, string[] baseNames, MethodDefinition[] methods)
        {
            Namespace = forNamespace;
            ClassName = className;
            BaseNames = baseNames;
            Methods = methods;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] BaseNames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MethodDefinition[] Methods { get; set; }

        /// <summary>
        /// helper to generate the source file for an entire class
        /// based on a collectin of method definitions
        /// TODO: maybe this should be in a separate ClassDefinition class?
        /// </summary>
        /// <param name="classNamespace"></param>
        /// <param name="className"></param>
        /// <param name="methodDefinitions"></param>
        /// <returns></returns>
        public SyntaxTree CreateSyntaxTree()
        {
            string sourceText = ToString();

            // and finally parse the tree
            return CSharpSyntaxTree.ParseText(sourceText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // default scope for classes is "public"
            var classScope = "public";

            // format the class definition as "class { method { } method { }"
            var classDefinition = $"{classScope} class {ClassName} " +
                $"{(BaseNames != null ? ":" : string.Empty)} {Join(", ", BaseNames)} " +
                $"{{ {Join("\n", from md in Methods select md.ToString())} }}";

            // form the total source text by wrapping the class definition in a namespace
            var sourceText = $"namespace {Namespace} {{ {classDefinition}  }}";
            return sourceText;
        }

        /// <summary>
        /// helper that joins strings, even if the substrings are null
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="strings"></param>
        private static string Join(string separator, IEnumerable<string> substrings) =>
            substrings != null
                ? string.Join(separator, substrings)
                : string.Empty;
    }
}
