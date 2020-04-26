using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace PuppeteerService
{
    public static class PuppeteerBrowser
    {
        public static async Task<Browser> GetBrowser(string chromiumPath, bool headless, IEnumerable<string> args = null)
        {
            if (string.IsNullOrEmpty(chromiumPath)) chromiumPath = Environment.CurrentDirectory;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-706915\\chrome-win\\chrome.exe");
            //chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            var options = new LaunchOptions
            {
                Headless = headless,
                ExecutablePath = chromiumPath,
                DefaultViewport = new ViewPortOptions { IsLandscape = true },
                IgnoreHTTPSErrors = true,
                SlowMo = 10
            };

            //var connectOptions = new ConnectOptions { BrowserWSEndpoint = $"wss://chrome.browserless.io?token={apikey}", BrowserURL="http://127.0.0.1:2122" };
            //await Puppeteer.ConnectAsync(connectOptions);

            var optionsArgs = new List<string>
            {
                 "--disable-notifications"
                , "--no-sandbox"
                , "--disable-setuid-sandbox"
                , "--disable-infobars"
                , "--window-position=100,0"
                , "--ignore-certifcate-errors"
                , "--ignore-certifcate-errors-spki-list"
                , "--disable-web-security"
            };
            if (args != null) optionsArgs.AddRange(args);
            options.Args = optionsArgs.ToArray();

            if (Environment.OSVersion.VersionString.Contains("NT 6.1")) { options.WebSocketFactory = WebSocketFactory; }
            
            return await Puppeteer.LaunchAsync(options);
        }

        public static async Task Authenticate(Page page, string proxy)
        {
            //proxy auth
            if (!string.IsNullOrEmpty(proxy) && proxy.Contains("@"))
            {
                var credentials = proxy.Split('@')[0];
                var userAndPassword = credentials.Split(':');
                if (userAndPassword.Length == 2)
                {
                    var username = userAndPassword[0];
                    var password = userAndPassword[1];
                    await page.AuthenticateAsync(new Credentials { Username = username, Password = password });
                }
            }
        }

        private static async Task<WebSocket> WebSocketFactory(Uri url, IConnectionOptions options, CancellationToken cancellationToken)
        {
            var ws = new System.Net.WebSockets.Managed.ClientWebSocket();
            await ws.ConnectAsync(url, cancellationToken);
            return ws;
        }
    }
}
