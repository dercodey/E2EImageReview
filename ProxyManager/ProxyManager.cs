namespace ProxyManager
{
    /// <summary>
    /// ProxyManager maintains proxies (that can be reused?)
    /// </summary>
    public class ProxyManager
    {
        public TInterface GetProxy<TInterface>()
        {
            return default(TInterface);
        }
    }
}
