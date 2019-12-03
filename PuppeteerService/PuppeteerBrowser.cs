using PuppeteerSharp;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace PuppeteerService
{
    public static class PuppeteerBrowser
    {
        public async static Task<Browser> GetBrowser(string chromiumPath, bool headless, string[] args = null)
        {
            if (string.IsNullOrEmpty(chromiumPath)) chromiumPath = Environment.CurrentDirectory;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            var options = new LaunchOptions
            {
                Headless = headless,
                ExecutablePath = chromiumPath,
                DefaultViewport = new ViewPortOptions { IsLandscape = true },
                IgnoreHTTPSErrors = true
            };
            if (args == null)
            {
                options.Args = new[] {
                  "--disable-notifications"
                , "--no-sandbox"
                , "--disable-setuid-sandbox"
                , "--disable-infobars"
                , "--window-position=0,0"
                , "--ignore-certifcate-errors"
                , "--ignore-certifcate-errors-spki-list"
                //, "--user-agent='Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3312.0 Safari/537.36'"
                };
            }
            else options.Args = args;
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
