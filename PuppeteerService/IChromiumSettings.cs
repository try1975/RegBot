﻿using Common.Service.Enums;
using System.Collections.Generic;

namespace PuppeteerService
{
    public interface IChromiumSettings
    {
        string GetPath();
        bool GetHeadless();
        ServiceCode ServiceCode { get; set; }
        string Proxy { get; set; }
        string GetUserAgent();
        IEnumerable<string> GetArgs();
        void AddArg(string arg);
        void ClearArgs();

        string GetProxy(ServiceCode serviceCode);
        void MarkProxySuccess(ServiceCode serviceCode, string proxy);
        void MarkProxyFail(ServiceCode serviceCode, string proxy);
    }
}
