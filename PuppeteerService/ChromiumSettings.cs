using Common.Service.Enums;
using Common.Service.Interfaces;
using System.Collections.Generic;
using System.Configuration;

namespace PuppeteerService
{
    public class ChromiumSettings : IChromiumSettings
    {
        private readonly string _chromiumPath;
        private readonly IUserAgent _userAgentGenerator;
        private readonly IProxyStore _proxyStore;
        private string userAgent = ConfigurationManager.AppSettings[nameof(userAgent)];
        private readonly bool Headless;

        public ChromiumSettings(string chromiumPath, IUserAgent userAgentGenerator, IProxyStore proxyStore)
        {
            _chromiumPath = chromiumPath;
            _userAgentGenerator = userAgentGenerator;
            _proxyStore = proxyStore;
            bool.TryParse(ConfigurationManager.AppSettings[nameof(Headless)], out Headless);
        }
        public bool GetHeadless()
        {
            return Headless;
        }

        public string GetPath()
        {
            return _chromiumPath;
        }

        public string GetUserAgent()
        {
            if (string.IsNullOrEmpty(userAgent) && _userAgentGenerator != null) return _userAgentGenerator.GetRandomUserAgent();
            return userAgent;
        }

        public IEnumerable<string> GetArgs()
        {
            //прокси
            List<string> args = null;
            if (!string.IsNullOrEmpty(Proxy))
            {
                string proxy;
                if (Proxy.Contains("@")) proxy = Proxy.Split('@')[1];
                else proxy = Proxy;
                if (args == null) args = new List<string>();
                args.Add($"--proxy-server={proxy}");
            }
            return args;
        }

        public string GetProxy(ServiceCode serviceCode)
        {
            return _proxyStore != null ? _proxyStore.GetProxy(serviceCode) : string.Empty;
        }

        public void MarkProxySuccess(ServiceCode serviceCode, string proxy)
        {
            if (_proxyStore != null) _proxyStore.MarkProxySuccess(serviceCode, proxy);
        }

        public void MarkProxyFail(ServiceCode serviceCode, string proxy)
        {
            if (_proxyStore != null) _proxyStore.MarkProxyFail(serviceCode, proxy);
        }

        public string Proxy { get; set; }
    }
}
