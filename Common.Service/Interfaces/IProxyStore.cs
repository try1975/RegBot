using Common.Service.Enums;
using System.Collections.Generic;

namespace Common.Service.Interfaces
{
    public interface IProxyStore
    {
        string GetProxy(ServiceCode serviceCode);
        List<IProxyData> GetProxies();

        IProxyData StoreItem(IProxyData proxyData);
        bool RemoveItem(int id);
        void MarkProxySuccess(ServiceCode serviceCode, string proxy);
        void MarkProxyFail(ServiceCode serviceCode, string proxy);
        string GetPath();
    }
}
