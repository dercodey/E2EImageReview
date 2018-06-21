using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;

namespace ProxyManager.Tests
{
    [ServiceContract]
    public interface IServiceTest
    {
        [OperationContract]
        int AddOne(int x);
    }

    [TestClass()]
    public class ProxyClassGeneratorTests
    {
        [TestMethod()]
        public void CompileSyntaxTreeTest()
        {
            var testNamespace = "TestNamespace";
            var className = "WriterClass";
            var methodName = "Write";

            var syntaxTree =
                MethodDefinition.CreateSyntaxTreeForClass(testNamespace, className, null, 
                    new MethodDefinition[] 
                    {
                        new MethodDefinition("void", methodName, null, 
                            new string[] 
                            {
                                "System.Console.WriteLine(\"Hello World\")"
                            })
                    });

            var metadataReferences =
                new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                };

            var assembly = ProxyClassGenerator.CompileAssemblyForSyntaxTree(syntaxTree, metadataReferences);
            Type type = assembly.GetType(string.Join(".", testNamespace, className));
            object instance = Activator.CreateInstance(type);
            type.InvokeMember(methodName, BindingFlags.Default | BindingFlags.InvokeMethod,
                null, instance, null);
        }

        [TestMethod()]
        public void CreateProxyForContractTest()
        {
            // create the proxy
            var proxy = ProxyClassGenerator.CreateProxy<IServiceTest>();

            // get the contract type
            var cb = (ClientBase<IServiceTest>)proxy;
            var contractName = cb.Endpoint.Contract.ContractType.FullName;

            // check that proxy type is correct
            Assert.IsTrue(cb.Endpoint.Contract.ContractType.IsAssignableFrom(typeof(IServiceTest))); 
                // contractName.CompareTo(typeof(IServiceTest).FullName) == 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(EndpointNotFoundException))]
        public void CallProxyWithNoEndpoint()
        {
            // create the proxy
            var proxy = ProxyClassGenerator.CreateProxy<IServiceTest>();

            // try invoking--should fail as no service instance is running
            var result = proxy.AddOne(2);
        }
    }
}