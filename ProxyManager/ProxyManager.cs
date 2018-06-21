namespace ProxyManager
{
    /// <summary>
    /// ProxyManager maintains proxies (that can be reused?)
    /// TODO: add Roslyn compiler interface
    /// </summary>
    public class ProxyManager
    {
        public TInterface GetProxy<TInterface>()
        {
            return default(TInterface);
        }
    }
}
