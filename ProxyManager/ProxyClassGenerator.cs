using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace ProxyManager
{
    /// <summary>
    /// static class to generate a proxy for a WCF service contract interface 
    /// </summary>
    public static class ProxyClassGenerator
    {
        // list of generated assemblies
        static List<Assembly> _generatedAssemblies = new List<Assembly>();

        // map from interfaces to proxy classes
        static Dictionary<Type, Type> _interfaceProxyMap = new Dictionary<Type, Type>();

        /// <summary>
        /// Retrieves the proxy type for the given interface
        /// </summary>
        /// <typeparam name="TInterface">should be a ServiceContract interface</typeparam>
        /// <returns></returns>
        public static Type GetProxyTypeFor<TInterface>()
        {
            // try to get from the generated class dictionary
            Type proxyType = null;
            if (!_interfaceProxyMap.TryGetValue(typeof(TInterface), out proxyType))
            {
                // generate the proxy class

                // create the metadata references (assemblies needed for the proxy class)
                var references = GetMetadataReferences<TInterface>();

                // get the syntax tree and compile it
                var syntaxTree = CreateSyntaxTreeForProxyClass<TInterface>();
                Assembly assembly = CompileAssemblyForSyntaxTree(syntaxTree, references);

                // now get the proxy class and add to the dictionary
                proxyType = assembly.GetType($"{ProxyNamespace}.{GetProxyName<TInterface>()}");
                _interfaceProxyMap.Add(typeof(TInterface), proxyType);
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
            $"{typeof(TInterface).Name}Proxy";

        /// <summary>
        /// creates a syntax tree for the class defining the proxy of TInterface
        /// </summary>
        /// <typeparam name="TInterface">the service contract interface for which a proxy is needed</typeparam>
        /// <returns>the Roslyn SyntaxTree for the class</returns>
        private static SyntaxTree CreateSyntaxTreeForProxyClass<TInterface>()
        {
            var interfaceType = typeof(TInterface);

            // function that creates the statement used to map service calls to the underlying channel
            Func<MethodInfo, string> funcChannelInvoke =
                mi => $"return Channel.{mi.Name}({string.Join(",", mi.GetParameters().Select(pi => pi.Name))})";

            // for each method on the interface, use MethodDefinition to define the method
            var methodStatements =
                interfaceType
                    .GetMethods()
                    .Select(mi =>
                        MethodDefinition.FromMethodInfo(mi,
                            new string[] { funcChannelInvoke(mi) }));

            // now create the total class definition
            var classDefinition = new ClassDefinition(ProxyNamespace,
                    GetProxyName<TInterface>(),
                    new string[] { $"System.ServiceModel.ClientBase<{interfaceType.FullName}>", interfaceType.FullName },
                    methodStatements.ToArray());

            // and return the syntax tree for the class
            return classDefinition.CreateSyntaxTree();
        }

        /// <summary>
        /// returns a collection of MetaDataReference objects needed to compile the proxy
        /// this includes some standard references, the TInterface, and any types utilized by the TInterface methods
        /// </summary>
        /// <typeparam name="TInterface">the service contract to be used</typeparam>
        /// <returns>array of MetadataReference objects</returns>
        private static MetadataReference[] GetMetadataReferences<TInterface>()
        {
            // these are the standard metadata references
            var standardMetadataReferences =
                new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(ChannelFactory).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(DataContractAttribute).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(TInterface).Assembly.Location),
                };

            // these are the return types (and generic descendants) used by methods on TInterface
            var returnTypes =
                typeof(TInterface)
                    .GetMethods()
                    .Select(mi => mi.ReturnType)
                    .SelectMany(GetDescendentGenericArguments);

            // these are the parameter types (and generic descendants) used by methods on TInterface
            var parameterTypes =
                typeof(TInterface)
                    .GetMethods()
                    .SelectMany(mi => mi.GetParameters())
                    .Select(pi => pi.ParameterType)
                    .SelectMany(GetDescendentGenericArguments);

            // now get the MetadataReferences for all of the above
            var interfaceMetadataReferences =
                returnTypes
                    .Concat(parameterTypes)
                    .Select(tp => MetadataReference.CreateFromFile(tp.Assembly.Location));

            // and combine the standard and interface metadata, remove duplicates, and turn to array
            return standardMetadataReferences.Concat(interfaceMetadataReferences).Distinct().ToArray();
        }

        /// <summary>
        /// helper to recursively get generic type parameters
        /// </summary>
        /// <param name="fromType">initial type</param>
        /// <returns>collection containing intial type and any descendent generic type parameters</returns>
        static IEnumerable<Type> GetDescendentGenericArguments(Type fromType)
        {
            yield return fromType;

            if (fromType.IsGenericType)
            {
                foreach (var ga in fromType.GetGenericArguments())
                {
                    foreach (var dga in GetDescendentGenericArguments(ga))
                    {
                        yield return dga;
                    }
                }
            }
        }

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
