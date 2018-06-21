using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
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
        public MethodDefinition(string returnType, string methodName, string[] arguments, string[] statements)
        {
            ReturnType = returnType;
            MethodName = methodName;
            Arguments = arguments;
            Statements = statements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static MethodDefinition FromMethodInfo(MethodInfo mi, string[] statements)
        {
            var parameterDeclaration = 
                from pi in mi.GetParameters()
                select $"{ToGenericTypeString(pi.ParameterType)} {pi.Name}";

            return new MethodDefinition(ToGenericTypeString(mi.ReturnType), mi.Name, 
                parameterDeclaration.ToArray(), statements);
        }

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
            var classDefinition = $"{classScope} class {className} " +
                $"{(baseNames != null ? ":" : string.Empty)} {Join(", ", baseNames)} " +
                $"{{ {Join("\n", from md in methodDefinitions select md.ToString())} }}";
            var sourceText = $"namespace {classNamespace} {{ {classDefinition}  }}";

            return CSharpSyntaxTree.ParseText(sourceText);
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
        public string[] Statements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{MethodScope} {ReturnType} {MethodName} ({Join(",", Arguments)})" +
            $" {{ {Join("\n", from statement in Statements select $"{statement};")} }}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static string ToGenericTypeString(Type t)
        {
            if (!t.IsGenericType)
                return $"{t.Namespace}.{t.Name}";
            string genericTypeName = t.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
            string genericArgs = Join(",", from ta in t.GetGenericArguments() select ToGenericTypeString(ta));
            return $"{t.Namespace}.{genericTypeName}<{genericArgs}>";
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
