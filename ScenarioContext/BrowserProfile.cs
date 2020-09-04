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
            /*
             --proxy-server=host:port
          Specify the HTTP/SOCKS4/SOCKS5 proxy server to use for requests.  This overrides any environment variables or settings picked via the options dialog.  An individual
          proxy server is specified using the format:

            [<proxy-scheme>://]<proxy-host>[:<proxy-port>]

          Where <proxy-scheme> is the protocol of the proxy server, and is one of:

            "http", "socks", "socks4", "socks5".

          If the <proxy-scheme> is omitted, it defaults to "http". Also note that "socks" is equivalent to "socks5".

          Examples:

            --proxy-server="foopy:99"
                Use the HTTP proxy "foopy:99" to load all URLs.

            --proxy-server="socks://foobar:1080"
                Use the SOCKS v5 proxy "foobar:1080" to load all URLs.

            --proxy-server="sock4://foobar:1080"
                Use the SOCKS v4 proxy "foobar:1080" to load all URLs.

            --proxy-server="socks5://foobar:66"
                Use the SOCKS v5 proxy "foobar:66" to load all URLs.

          It is also possible to specify a separate proxy server for different URL types, by prefixing the proxy server specifier with a URL specifier:

          Example:

            --proxy-server="https=proxy1:80;http=socks4://baz:1080"
                Load https://* URLs using the HTTP proxy "proxy1:80". And load http://*
                URLs using the SOCKS v4 proxy "baz:1080".
             */

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
