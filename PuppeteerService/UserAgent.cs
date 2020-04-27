using Common.Service.Interfaces;
using System;
using System.IO;

namespace PuppeteerService
{
    public class UserAgent: IUserAgent
    {
        private static readonly string[] _userAgents;
        private static readonly Random _random = new Random();
        static UserAgent()
        {
            var userAgentsPath = Path.Combine(Environment.CurrentDirectory, "UserAgents.txt");
            if (File.Exists(userAgentsPath)) _userAgents = File.ReadAllLines(userAgentsPath);
        }

        public string GetRandomUserAgent()
        {
            //if (_userAgents == null) return "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3312.0 Safari/537.36";
            return _userAgents[_random.Next(0, _userAgents.Length)];
        }

        /* https://gist.github.com/endel/b75d3a6af0fcae066dfa09cc066f56ee
         export function* generateUserAgent() { 
            let webkitVersion = 10; 
            let chromeVersion = 1000; 

            const so = [ 
             'Windows NT 6.1; WOW64', 
             'Windows NT 6.2; Win64; x64', 
             "Windows NT 5.1; Win64; x64",, 
             'Macintosh; Intel Mac OS X 10_12_6', 
             "X11; Linux x86_64", 
             "X11; Linux armv7l" 
            ]; 
            let soIndex = Math.floor(Math.random() * so.length); 

            while (true) { 
             yield `Mozilla/5.0 (${so[soIndex++ % so.length]}) AppleWebKit/537.${webkitVersion} (KHTML, like Gecko) Chrome/56.0.${chromeVersion}.87 Safari/537.${webkitVersion} OPR/43.0.2442.991`; 

             webkitVersion++; 
             chromeVersion++; 
         */
    }
}
