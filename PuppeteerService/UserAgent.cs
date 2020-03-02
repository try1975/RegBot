using System;
using System.IO;

namespace PuppeteerService
{
    public static class UserAgent
    {
        private static readonly string[] _userAgents;
        private static readonly Random _random = new Random();
        static UserAgent()
        {
            var userAgentsPath = Path.Combine(Environment.CurrentDirectory, "UserAgents.txt");
            if (File.Exists(userAgentsPath)) _userAgents = File.ReadAllLines(userAgentsPath);
        }
        public static string GetRandomUserAgent()
        {
            if (_userAgents == null) return "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3312.0 Safari/537.36";
            return _userAgents[_random.Next(0, _userAgents.Length)];
        }
    }
}
