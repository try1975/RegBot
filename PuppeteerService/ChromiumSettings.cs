using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Configuration;

namespace PuppeteerService
{
    public class ChromiumSettings : IChromiumSettings
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ChromiumSettings));
        private readonly string _chromiumPath;
        private readonly IUserAgentProvider _userAgentProvider;
        private readonly IProxyStore _proxyStore;
        private readonly string userAgent = ConfigurationManager.AppSettings[nameof(userAgent)];
        private readonly bool Headless;
        private bool _noProxy;
        private readonly List<string> _addedArgs = new List<string>();

        public string Proxy { get; set; }
        public ServiceCode ServiceCode { get; set; }

        public ChromiumSettings(string chromiumPath, IUserAgentProvider userAgentGenerator, IProxyStore proxyStore)
        {
            _chromiumPath = chromiumPath;
            _userAgentProvider = userAgentGenerator;
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
            if (string.IsNullOrEmpty(userAgent) && _userAgentProvider != null)
            {
                var randomUserAgent = _userAgentProvider.GetRandomUserAgent(ServiceCode);
                return randomUserAgent;
            }
            return userAgent;
        }

        public IEnumerable<string> GetArgs()
        {
            //прокси
            List<string> args = new List<string>();
            if (!string.IsNullOrEmpty(Proxy))
            {
                Log.Debug($"{nameof(Proxy)}: {Proxy}");
                var proxy = string.Empty;
                var idxComma = Proxy.IndexOf(',');
                var proxyProtocol = string.Empty;
                if (idxComma >= 0)
                {
                    proxy = Proxy.Substring(0, idxComma);
                    proxyProtocol = Proxy.Substring(idxComma + 1).Trim().ToLower();
                    if (proxyProtocol.StartsWith("socks")) proxyProtocol = $@"{proxyProtocol}://"; else proxyProtocol = string.Empty;
                }
                if (proxy.Contains("@")) proxy = proxy.Split('@')[1];
                args.Add($"--proxy-server={proxyProtocol}{proxy}");
            }
            if (_userAgentProvider != null)
            {
                var useragent = GetUserAgent();
                if (!string.IsNullOrEmpty(useragent))
                {
                    Log.Debug($"{nameof(useragent)}: {useragent}");
                    args.Add($@"--user-agent=""{useragent}""");
                }
            }
            if (_noProxy) { args.Add("--no-proxy-server"); }
            args.AddRange(_addedArgs);
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

        public void AddArg(string arg)
        {
            _addedArgs.Add(arg);
        }

        public void ClearArgs()
        {
            _addedArgs.Clear();
        }
    }
}
