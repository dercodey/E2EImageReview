using System;
using System.ServiceModel;

namespace ProxyManager
{
    /// <summary>
    /// ProxyManager maintains proxies (that can be reused?)
    /// TODO: add Roslyn compiler interface
    /// </summary>
    public class ProxyManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static ProxyManager Instance
        {
            get => _proxyManager;
        }
        static ProxyManager _proxyManager = new ProxyManager();

        /// <summary>
        /// gets a proxy
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public TInterface GetProxy<TInterface>() =>
            CreateProxy<TInterface>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public TInterface CreateProxy<TInterface>()
        {
            Type proxyType = ProxyClassGenerator.GetProxyTypeFor<TInterface>();
            TInterface proxy = (TInterface)Activator.CreateInstance(proxyType);
            return (TInterface)proxy;
        }

        /// <summary>
        /// helper to close a generated proxy
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="proxy"></param>
        public void CloseProxy<TInterface>(TInterface proxy)
            where TInterface : class
        {
            var clientBase = proxy as ClientBase<TInterface>;
            if (clientBase != null)
            {
                clientBase.Close();
            }
        }
    }
}
