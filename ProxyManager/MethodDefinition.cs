using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using System.Reflection;

namespace ProxyManager
{
    /// <summary>
    /// 
    /// </summary>
    public class MethodDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnType"></param>
        /// <param name="methodName"></param>
        /// <param name="arguments"></param>
        /// <param name="blocks"></param>
        public MethodDefinition(string returnType, string methodName, string[] arguments, string[] blocks)
        {
            ReturnType = returnType;
            MethodName = methodName;
            Arguments = arguments;
            Blocks = blocks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static MethodDefinition FromMethodInfo(MethodInfo mi, string[] blocks)
        {
            var parameterDeclaration = mi.GetParameters().Select(pi => $"{pi.ParameterType.FullName} {pi.Name}");
            return new MethodDefinition(mi.ReturnType.FullName, mi.Name, parameterDeclaration.ToArray(), blocks);
        }

        /// <summary>
        /// 
        /// </summary>
        public string MethodScope => "public";

        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Arguments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Blocks { get; set; }

        /// <summary>
        /// helper to generate the source file for an entire class
        /// based on a collectin of method definitions
        /// </summary>
        /// <param name="classNamespace"></param>
        /// <param name="className"></param>
        /// <param name="methodDefinitions"></param>
        /// <returns></returns>
        public static SyntaxTree CreateSyntaxTreeForClass(string classNamespace,
            string className, string[] baseNames,
            MethodDefinition[] methodDefinitions)
        {
            var classScope = "public";
            var mdString = string.Join("\n", methodDefinitions.Select(md => md.ToString()));
            var classDefinition = $"{classScope} class {className} : {(baseNames != null ? string.Join(", ", baseNames) : "object")} {{ {mdString} }}";
            var sourceText =  $"namespace {classNamespace} {{ {classDefinition}  }}";

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceText);
            return syntaxTree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{MethodScope} {ReturnType} {MethodName} " +
            $"({(Arguments != null ? string.Join(",", Arguments) : string.Empty)})" +
            $" {{ {string.Join("\n", Blocks.Select(block => $"{block};"))} }}";
    }

}
