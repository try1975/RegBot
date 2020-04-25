using Common.Service.Interfaces;
using System;

namespace PuppeteerService
{
    public class UserAgent2 : IUserAgent
    {
        private readonly Random _random = new Random();
        private int webkitVersion = 10;
        private int chromeVersion = 1000;
        private readonly string[] so = new string[] {
            "Windows NT 6.1; WOW64",
            "Windows NT 6.2; Win64; x64",
            "Windows NT 5.1; Win64; x64",
            "Macintosh; Intel Mac OS X 10_12_6",
            "X11; Linux x86_64",
            "X11; Linux armv7l"
        };

        public UserAgent2()
        {
            webkitVersion = _random.Next(webkitVersion, webkitVersion * 3);
            chromeVersion = _random.Next(chromeVersion, chromeVersion + webkitVersion);
        }

        public string GetRandomUserAgent()
        {
            webkitVersion++;
            chromeVersion++;
            return $"Mozilla / 5.0({so[_random.Next(0, so.Length)]}) AppleWebKit / 537.{webkitVersion} (KHTML, like Gecko) Chrome / 56.0.{chromeVersion}.87 Safari / 537.{webkitVersion} OPR / 43.0.2442.991";
        }
    }
}
