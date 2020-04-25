using Common.Service.Enums;
using System.Collections.Generic;

namespace PuppeteerService
{
    public interface IChromiumSettings
    {
        string GetPath();
        bool GetHeadless();
        string Proxy { get; set; }
        string GetUserAgent();
        IEnumerable<string> GetArgs();

        string GetProxy(ServiceCode serviceCode);
        void MarkProxySuccess(ServiceCode serviceCode, string proxy);
        void MarkProxyFail(ServiceCode serviceCode, string proxy);
    }
}
