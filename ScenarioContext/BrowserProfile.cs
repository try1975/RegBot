using Common.Service;
using Common.Service.Interfaces;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScenarioContext
{
    public class BrowserProfile: IBrowserProfile
    {
        #region Fields&Properties
        private Browser _browser;
        private string _webSocketEndpoint;
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
        public ProxyRecord ProxyRecord { get; set; }
        public int RemoteDebuggingPort { get; set; }
        #endregion

        public BrowserProfile()
        {
            ProxyRecord = new ProxyRecord();
            RemoteDebuggingPort = 9222;
        }

        public async Task<Browser> ProfileStart(string chromiumPath, string profilesPath)
        {
            if (_browser != null) return _browser;
            
            var args = new List<string>();
            if (!string.IsNullOrEmpty(UserAgent)) args.Add($@"--user-agent=""{UserAgent}""");
            if (!string.IsNullOrEmpty(Language)) args.Add($"--lang={Language}");
            if(RemoteDebuggingPort>0) args.Add($"--remote-debugging-port={RemoteDebuggingPort}");

            var proxyArg = ProxyRecord.GetProxyArg();
            if(!string.IsNullOrEmpty(proxyArg)) args.Add(proxyArg);

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
            _webSocketEndpoint = _browser.WebSocketEndpoint;
            //await Puppeteer.ConnectAsync(new ConnectOptions());
            //_browser.Disconnected += Browser_Disconnected;
            _browser.Closed += _browser_Closed;
            var page = (await _browser.PagesAsync())[0];
            //await page.SetRequestInterceptionAsync(true);
            //page.Request += Page_Request;

            //var headers = new Dictionary<string, string>();
            //headers["RtttU"] = " you site";
            //headers["Accept"] = "text/html";
            //await page.SetExtraHttpHeadersAsync(headers);
            if (!string.IsNullOrEmpty(proxyArg) && !string.IsNullOrEmpty(ProxyRecord.Username) && !string.IsNullOrEmpty(ProxyRecord.Password))
            {
                await page.AuthenticateAsync(new Credentials { Username = ProxyRecord.Username, Password = ProxyRecord.Password });
            }
            if (!string.IsNullOrEmpty(Timezone)) await page.EmulateTimezoneAsync(Timezone);
            //await page.EvaluateExpressionAsync("window.navigator.__defineGetter__('plugins', () => '');");
            await page.EvaluateExpressionOnNewDocumentAsync("window.navigator.__defineGetter__('plugins', () => []);");
            //await page.EvaluateExpressionOnNewDocumentAsync(File.ReadAllText(@"C:\Projects\RegBot\Fingerprint.Classes\JavaScript1.js"));


            //if (!string.IsNullOrEmpty(StartUrl)) await page.GoToAsync(StartUrl, _navigationOptions);


            return _browser;
        }

        private void _browser_Closed(object sender, EventArgs e)
        {
            _browser = null;
        }

        private void Browser_Disconnected(object sender, EventArgs e)
        {
            _browser = null;
        }
    }
}
