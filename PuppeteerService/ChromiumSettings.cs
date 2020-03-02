namespace PuppeteerService
{
    public class ChromiumSettings : IChromiumSettings
    {
        private readonly string _chromiumPath;
        private string userAgent = System.Configuration.ConfigurationManager.AppSettings[nameof(userAgent)];

        public ChromiumSettings(string chromiumPath, string userAgent = "")
        {
            _chromiumPath = chromiumPath;
            if (!string.IsNullOrEmpty(userAgent)) this.userAgent = userAgent;
        }
        public bool GetHeadless()
        {
            return false;
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
