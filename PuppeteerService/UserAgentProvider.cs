using Common.Service.Enums;
using Common.Service.Interfaces;
using System;
using System.IO;

namespace PuppeteerService
{
    public class UserAgentProvider : IUserAgentProvider
    {
        // https://gist.github.com/endel/b75d3a6af0fcae066dfa09cc066f56ee
        private readonly string[] _userAgents;
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

        public UserAgentProvider()
        {
            webkitVersion = _random.Next(webkitVersion, webkitVersion * 3);
            chromeVersion = _random.Next(chromeVersion, chromeVersion + webkitVersion);
            var userAgentsPath = Path.Combine(Environment.CurrentDirectory, "UserAgents.txt");
            if (File.Exists(userAgentsPath)) _userAgents = File.ReadAllLines(userAgentsPath);
        }

        public string GetRandomUserAgent()
        {
            webkitVersion++;
            chromeVersion++;
            return $"Mozilla/5.0 ({so[_random.Next(0, so.Length)]}) AppleWebKit/537.{webkitVersion} (KHTML, like Gecko) Chrome/56.0.{chromeVersion}.87 Safari/537.{webkitVersion} OPR/43.0.2442.991";
        }

        public string GetRandomUserAgent(ServiceCode serviceCode)
        {
            /*if (serviceCode == ServiceCode.Facebook) return _userAgents[_random.Next(0, _userAgents.Length)];
            else*/ if (serviceCode == ServiceCode.MailRu) return _userAgents[_random.Next(0, _userAgents.Length)];
            else return GetRandomUserAgent();
        }
    }
}
