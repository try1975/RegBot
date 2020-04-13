using System.Collections.Generic;

namespace PuppeteerService
{
    public interface IChromiumSettings
    {
        string GetPath();
        bool GetHeadless();
        string Proxy { get; set; }
        string GetUserAgent();
        IEnumerable<string> GetArgs();
    }
}
