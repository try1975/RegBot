namespace PuppeteerService
{
    public class ChromiumSettings : IChromiumSettings
    {
        private readonly string _chromiumPath;
        private readonly string _userAgent;

        public ChromiumSettings(string chromiumPath, string userAgent)
        {
            _chromiumPath = chromiumPath;
            _userAgent = userAgent;
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
            return _userAgent;
        }

        public string Proxy { get;set; }
    }
}
