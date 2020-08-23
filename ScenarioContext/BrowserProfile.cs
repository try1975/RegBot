using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScenarioContext
{
    public class BrowserProfile: IBrowserProfile
    {
        private Browser _browser;
        protected static readonly NavigationOptions _navigationOptions = new NavigationOptions
        {
            WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Load/*, WaitUntilNavigation.Networkidle2*/ }
        };

        public string Name { get; set; }
        public string Folder { get; set; }
        public string UserAgent { get; set; }
        public string StartUrl { get; set; }
        public string Language { get; set; }
        public string TimezoneCountry { get; set; }
        public string Timezone { get; set; }

        public async Task<Browser> ProfileStart(string chromiumPath, string profilesPath)
        {
            if (_browser != null) return _browser;
            
            var args = new List<string>();
            if (!string.IsNullOrEmpty(UserAgent)) args.Add($@"--user-agent=""{UserAgent}""");
            if (!string.IsNullOrEmpty(Language)) args.Add($"--lang={Language}");
            args.Add("--disable-webgl"); args.Add("--disable-3d-apis");

            var lanchOptions = new LaunchOptions
            {
                Headless = false,
                ExecutablePath = chromiumPath,
                DefaultViewport = new ViewPortOptions { IsLandscape = true },
                IgnoreHTTPSErrors = true,
                SlowMo = 10,
                UserDataDir = Path.Combine(profilesPath, Folder),
                Args = args.ToArray()
            };
           
            _browser =  await Puppeteer.LaunchAsync(lanchOptions);
            _browser.Disconnected += Browser_Disconnected;
            var page = (await _browser.PagesAsync())[0];
            if (!string.IsNullOrEmpty(Timezone)) await page.EmulateTimezoneAsync(Timezone);
            if (!string.IsNullOrEmpty(StartUrl)) await page.GoToAsync(StartUrl, _navigationOptions);
            return _browser;
        }

        private void Browser_Disconnected(object sender, EventArgs e)
        {
            _browser = null;
        }
    }
}
