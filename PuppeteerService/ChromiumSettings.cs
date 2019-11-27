namespace PuppeteerService
{
    public class ChromiumSettings : IChromiumSettings
    {
        private readonly string _chromiumPath;
        public ChromiumSettings(string chromiumPath)
        {
            _chromiumPath = chromiumPath;
        }
        public bool GetHeadless()
        {
            return false;
        }

        public string GetPath()
        {
            return _chromiumPath;
        }
    }
}
