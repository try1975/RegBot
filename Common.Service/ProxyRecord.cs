using Common.Service.Enums;
using Common.Service.Interfaces;
using System;

namespace Common.Service
{
    public class ProxyRecord
    {
        public ProxyProtocol ProxyProtocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string GetProxyArg()
        {
            var proxyArg = string.Empty;
            if (!string.IsNullOrEmpty(Host))
            {
                proxyArg = Host;
                if (Port > 0) proxyArg = $"{proxyArg}:{Port}";
                var proxyProtocol = Enum.GetName(typeof(ProxyProtocol), ProxyProtocol).ToLower();
                if (proxyProtocol.StartsWith("socks")) proxyArg = $"{proxyProtocol}://{proxyArg}";
                proxyArg = $"--proxy-server={proxyArg}";
            }
            return proxyArg;
        }

        public override string ToString()
        {
            var toString = string.Empty;
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password)) toString = $"{Username}:{Password}@";
            toString = $"{toString}{Host}";
            if (Port > 0) toString = $"{toString}:{Port}";
            var proxyProtocol = Enum.GetName(typeof(ProxyProtocol), ProxyProtocol).ToLower();
            if (!string.IsNullOrEmpty(toString)) toString = $"{toString},{proxyProtocol}";
            return toString;
        }

        public ProxyRecord GetCopy()
        {
            var proxyRecord = new ProxyRecord
            {
                ProxyProtocol = ProxyProtocol,
                Host = Host,
                Port = Port,
                Username = Username,
                Password = Password
            };
            return proxyRecord;
        }
        public void CopyDataTo(ProxyRecord proxyRecord)
        {
            proxyRecord.ProxyProtocol = ProxyProtocol;
            proxyRecord.Host = Host;
            proxyRecord.Port = Port;
            proxyRecord.Username = Username;
            proxyRecord.Password = Password;
        }
    }
}
