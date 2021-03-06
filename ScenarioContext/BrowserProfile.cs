﻿using Common.Service;
using Common.Service.Interfaces;
using Fingerprint.Classes;
using log4net;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScenarioContext
{
    public class BrowserProfile : IBrowserProfile
    {
        #region Fields&Properties
        private static readonly ILog Log = LogManager.GetLogger(typeof(BrowserProfile));
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

        private Fingerprint.Classes.Fingerprint _fingerprint { get; set; }

        #endregion

        public BrowserProfile()
        {
            ProxyRecord = new ProxyRecord();
            RemoteDebuggingPort = 0;
        }
        private Fingerprint.Classes.Fingerprint GetFingerprint(string profilesPath)
        {
            var path = Path.Combine(profilesPath, Folder, "fingerprint.json");
            if (!File.Exists(path)) return null;
            return JsonConvert.DeserializeObject<Fingerprint.Classes.Fingerprint>(File.ReadAllText(path));
        }

        public async Task<Browser> ProfileStart(string chromiumPath, string profilesPath)
        {
            if (_browser != null) return _browser;
            _fingerprint = GetFingerprint(profilesPath);
            var args = new List<string>();
            if (!string.IsNullOrEmpty(UserAgent)) args.Add($@"--user-agent=""{UserAgent}""");
            if (!string.IsNullOrEmpty(Language)) args.Add($"--lang={Language}");
            if (RemoteDebuggingPort > 0) args.Add($"--remote-debugging-port={RemoteDebuggingPort}");

            var proxyArg = ProxyRecord.GetProxyArg();
            if (!string.IsNullOrEmpty(proxyArg)) args.Add(proxyArg);

            args.Add("--disable-webgl");
            args.Add("--disable-3d-apis");
            #region proxy info
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
            #endregion
            var viewPortOptions = new ViewPortOptions { IsLandscape = true };
            if (_fingerprint != null)
            {
                viewPortOptions = new ViewPortOptions();
                viewPortOptions.Height = _fingerprint.height;
                viewPortOptions.Width = _fingerprint.width;
                viewPortOptions.DeviceScaleFactor = _fingerprint.attr.windowdevicePixelRatio;
                args.Add($"--window-size={_fingerprint.width},{_fingerprint.height}");
            }
            var lanchOptions = new LaunchOptions
            {
                Headless = false,
                ExecutablePath = chromiumPath,
                DefaultViewport = viewPortOptions,
                IgnoreHTTPSErrors = true,
                SlowMo = 10,
                UserDataDir = Path.Combine(profilesPath, Folder),
                Args = args.ToArray()
            };

            _browser = await Puppeteer.LaunchAsync(lanchOptions);

            _browser.TargetCreated += Browser_TargetCreated;
            _browser.TargetChanged += Browser_TargetChanged;

            _webSocketEndpoint = _browser.WebSocketEndpoint;
            //await Puppeteer.ConnectAsync(new ConnectOptions());
            _browser.Disconnected += Browser_Disconnected;
            _browser.Closed += Browser_Closed;
            var page = (await _browser.PagesAsync())[0];
            //await page.SetRequestInterceptionAsync(true);
            //page.Console += Page_Console;
            //await page.SetViewportAsync();
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

            RunScriptOnPage(page);
            if (!string.IsNullOrEmpty(StartUrl)) await page.GoToAsync(StartUrl, _navigationOptions);
            //var session = await page.Target.CreateCDPSessionAsync();
            //await session.SendAsync("Emulation.setPageScaleFactor", new { pageScaleFactor= 4 });

            return _browser;
        }
        
        private async void RunScriptOnPage(Page page)
        {
            if (_fingerprint == null) return;

            #region navigator.platfrom

            var navigatorplatform = _fingerprint.attr?.navigatorplatform;
            //await page.EvaluateExpressionOnNewDocumentAsync($"window.navigator.__defineGetter__('platform', () => '{navigatorplatform}');");

            await page.EvaluateFunctionOnNewDocumentAsync(
                @"(platform) => {
                    Object.defineProperties(navigator, {
                    platform:
                        {
                            get() { return platform; }
                        },
                    });
                }", navigatorplatform);
            #endregion

            var navigatorhardwareConcurrency = _fingerprint.attr?.navigatorhardwareConcurrency;
            //await page.EvaluateExpressionOnNewDocumentAsync($"window.navigator.__defineGetter__('hardwareConcurrency', () => '{navigatorhardwareConcurrency}');");

            var screenheight = _fingerprint.attr?.screenheight;
            if (screenheight.HasValue) await page.EvaluateExpressionOnNewDocumentAsync($"window.screen.__defineGetter__('height', () => '{screenheight}');");
            var screenwidth = _fingerprint.attr?.screenwidth;
            if (screenwidth.HasValue) await page.EvaluateExpressionOnNewDocumentAsync($"window.screen.__defineGetter__('width', () => '{screenwidth}');");

            var overrideNavigatorLanguages = @"Object.defineProperty(navigator, 'languages', {
                                                  get: function() {
                                                    return ['en-US', 'en', 'bn'];
                                                  };
                                                });";
            await page.EvaluateExpressionOnNewDocumentAsync(overrideNavigatorLanguages);

            //navigatorhardwareConcurrency = 18;
            //await page.EvaluateFunctionOnNewDocumentAsync(@"(hardwareConcurrency) => {
            //        window.navigator.__defineGetter__('hardwareConcurrency', () => hardwareConcurrency);
            //        }", navigatorhardwareConcurrency);

            //await page.EvaluateExpressionAsync("window.navigator.__defineGetter__('platform', () => 'Linux armv8l');");
            //await page.EvaluateExpressionOnNewDocumentAsync("window.navigator.__defineGetter__('plugins', () => []);");
            //await page.EvaluateExpressionOnNewDocumentAsync("window.navigator.__defineGetter__('platform', () => 'Linux armv8l');");
            //await page.EvaluateExpressionOnNewDocumentAsync(File.ReadAllText(@"C:\Projects\RegBot\Fingerprint.Classes\JavaScript1.js"));

        }

        private void Page_Console(object sender, ConsoleEventArgs e)
        {
            Log.Info($"Console message - {e.Message}");
        }

        private async void Browser_TargetChanged(object sender, TargetChangedArgs e)
        {
            try
            {
                var page = await e.Target.PageAsync();
                if (page != null)
                {
                    RunScriptOnPage(page);
                }
            }
            catch { }
        }

        private async void Browser_TargetCreated(object sender, TargetChangedArgs e)
        {
            try
            {
                var page = await e.Target.PageAsync();
                if (page != null)
                {
                    RunScriptOnPage(page);
                }
            }
            catch { }
        }

        private void Browser_Closed(object sender, EventArgs e)
        {
            _browser = null;
        }

        private void Browser_Disconnected(object sender, EventArgs e)
        {
            _browser = null;
        }
    }
}
