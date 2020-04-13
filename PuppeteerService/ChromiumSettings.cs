using System.Collections.Generic;
using System.Configuration;

namespace PuppeteerService
{
    public class ChromiumSettings : IChromiumSettings
    {
        private readonly string _chromiumPath;
        private string userAgent = ConfigurationManager.AppSettings[nameof(userAgent)];
        private readonly bool Headless;

        public ChromiumSettings(string chromiumPath, string userAgent = "")
        {
            _chromiumPath = chromiumPath;
            if (!string.IsNullOrEmpty(userAgent)) this.userAgent = userAgent;
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
            if (string.IsNullOrEmpty(userAgent)) return UserAgent.GetRandomUserAgent();
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

        public string Proxy { get; set; }
    }
}
