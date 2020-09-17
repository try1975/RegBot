using Common.Service;
using System;

namespace ScenarioApp.Controls.Interfaces
{
    public interface IOneProxyControl
    {
        ProxyRecord ProxyRecord { get; }
        event EventHandler ProxyValueUpdated;
        void SetProxyRecord(ProxyRecord proxyRecord);
    }
}
