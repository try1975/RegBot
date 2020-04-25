using Common.Service.Interfaces;

namespace ProxyStore.Service
{
    public class ProxyData : IProxyData
    {
        public int Id { get; set; }
        public string ProxyString { get; set; }
    }
}
