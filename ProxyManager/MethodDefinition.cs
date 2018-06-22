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
        /// construct a new method definition
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
        /// construct method definition from a reflection MethodInfo object
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static MethodDefinition FromMethodInfo(MethodInfo mi, string[] statements)
        {
            // for the method parameter declarations
            var parameterDeclaration = 
                from pi in mi.GetParameters()
                select $"{ToGenericTypeString(pi.ParameterType)} {pi.Name}";

            // return the new method definition
            return new MethodDefinition(ToGenericTypeString(mi.ReturnType), mi.Name, 
                parameterDeclaration.ToArray(), statements);
        }

        /// <summary>
        /// 
        /// </summary>
        public string MethodScope => "public";

        /// <summary>
        /// the method name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// the arguments of the method, in "[type] [name]" format
        /// </summary>
        public string[] Arguments { get; set; }

        /// <summary>
        /// the return type of the method
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// the sequence of statements forming the body of the method
        /// </summary>
        public string[] Statements { get; set; }

        /// <summary>
        /// converts the method definition to a compilable string
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{MethodScope} {ReturnType} {MethodName} ({Join(",", Arguments)})" +
            $" {{ {Join("\n", from statement in Statements select $"{statement};")} }}";

        /// <summary>
        /// helper to create a generic type string Generic<SubType> suitable for compilation
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
