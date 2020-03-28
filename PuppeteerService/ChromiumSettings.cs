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

        public string Proxy { get; set; }
    }
}
