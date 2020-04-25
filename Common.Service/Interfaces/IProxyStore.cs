using Common.Service.Enums;

namespace Common.Service.Interfaces
{
    public interface IProxyStore
    {
        string GetProxy(ServiceCode serviceCode);

        IProxyData StoreItem(IProxyData proxyData);
        bool RemoveItem(int id);
        void MarkProxySuccess(ServiceCode serviceCode, string proxy);
        void MarkProxyFail(ServiceCode serviceCode, string proxy);
    }
}
