using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace ProxyManager
{
    public static class ProxyClassGenerator
    {
        // list of generated assemblies
        static List<Assembly> _generatedAssemblies = new List<Assembly>();

        // map from interfaces to proxy classes
        static Dictionary<Type, Type> _interfaceProxyMap = new Dictionary<Type, Type>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static TInterface CreateProxy<TInterface>()
        {
            Type proxyType = GetProxyTypeFor<TInterface>();
            object proxy = Activator.CreateInstance(proxyType);
            return (TInterface)proxy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        internal static Type GetProxyTypeFor<TInterface>()
        {
            Type proxyType = null;
            if (!_interfaceProxyMap.TryGetValue(typeof(TInterface), out proxyType))
            {
                proxyType = CreateProxyTypeFor<TInterface>();
            }

            return proxyType;
        }

        /// <summary>
        /// designates the default namespace for all proxies
        /// </summary>
        private static string ProxyNamespace => 
            "GeneratedProxyClasses";

        /// <summary>
        /// generates the proxy name for a given interface
        /// </summary>
        /// <typeparam name="TInterface">the interface type</typeparam>
        /// <returns>the name of the proxy</returns>
        private static string GetProxyName<TInterface>() =>
            $"{typeof(TInterface).Name }Proxy";

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        internal static Type CreateProxyTypeFor<TInterface>()
        {
            var syntaxTree = CreateSyntaxTreeForProxyClass<TInterface>();
            var references = GetMetadataReferences<TInterface>();
            Assembly assembly = CompileAssemblyForSyntaxTree(syntaxTree, references);

            Type proxyType = assembly.GetType($"{ProxyNamespace}.{GetProxyName<TInterface>()}");
            _interfaceProxyMap.Add(typeof(TInterface), proxyType);
            return proxyType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        private static SyntaxTree CreateSyntaxTreeForProxyClass<TInterface>()
        {
            var interfaceType = typeof(TInterface);

            Func<MethodInfo, string> funcChannelInvoke =
                mi => $"return Channel.{mi.Name}({string.Join(",", mi.GetParameters().Select(pi => pi.Name))})";

            var methodStatements = 
                interfaceType
                    .GetMethods()
                    .Select(mi => 
                        MethodDefinition.FromMethodInfo(mi, 
                            new string[] { funcChannelInvoke(mi) }));

            return 
                MethodDefinition.CreateSyntaxTreeForClass(ProxyNamespace, 
                    GetProxyName<TInterface>(),
                    new string[] { $"System.ServiceModel.ClientBase<{interfaceType.FullName}>", interfaceType.FullName },
                    methodStatements.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        private static MetadataReference[] GetMetadataReferences<TInterface>() =>
            new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ChannelFactory).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(TInterface).Assembly.Location),
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syntaxTree"></param>
        /// <param name="references"></param>
        /// <returns></returns>
        public static Assembly CompileAssemblyForSyntaxTree(SyntaxTree syntaxTree, MetadataReference[] references)
        {
            string assemblyName = Path.GetRandomFileName();
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            Assembly assembly = null;
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);
                    failures.ToList().ForEach(diagnostic => Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage()));
                    return null;
                }

                ms.Seek(0, SeekOrigin.Begin);
                assembly = Assembly.Load(ms.ToArray());
                _generatedAssemblies.Add(assembly);
            }

            return assembly;
        }
    }
}
