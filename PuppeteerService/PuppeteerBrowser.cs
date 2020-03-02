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
        public async static Task<Browser> GetBrowser(string chromiumPath, bool headless, List<string> args = null)
        {
            if (string.IsNullOrEmpty(chromiumPath)) chromiumPath = Environment.CurrentDirectory;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-706915\\chrome-win\\chrome.exe");
            //chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            var options = new LaunchOptions
            {
                Headless = headless,
                ExecutablePath = chromiumPath,
                DefaultViewport = new ViewPortOptions { IsLandscape = true },
                IgnoreHTTPSErrors = true/*,
                SlowMo = 3*/
            };
            var optionsArgs = new List<string>
            {
                 "--disable-notifications"
                , "--no-sandbox"
                , "--disable-setuid-sandbox"
                , "--disable-infobars"
                , "--window-position=0,0"
                , "--ignore-certifcate-errors"
                , "--ignore-certifcate-errors-spki-list"
                //, $"--user-agent={UserAgent.GetRandomUserAgent()}"
                //, "--proxy-server=62.109.28.144:36629"
            };
            if (args != null) optionsArgs.AddRange(args);
            options.Args = optionsArgs.ToArray();

            if (Environment.OSVersion.VersionString.Contains("NT 6.1")) { options.WebSocketFactory = WebSocketFactory; }
            return await Puppeteer.LaunchAsync(options);
        }
        private static async Task<WebSocket> WebSocketFactory(Uri url, IConnectionOptions options, CancellationToken cancellationToken)
        {
            var ws = new System.Net.WebSockets.Managed.ClientWebSocket();
            await ws.ConnectAsync(url, cancellationToken);
            return ws;
        }
    }
}
