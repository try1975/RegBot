using PuppeteerSharp;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ScenarioApp
{
    static class PuppeteerBrowser
    {
        public async static Task<Browser> GetBrowser(string chromiumPath, bool headless)
        {
            if (string.IsNullOrEmpty(chromiumPath)) chromiumPath = Environment.CurrentDirectory;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            var options = new LaunchOptions
            {
                Headless = headless,
                ExecutablePath = chromiumPath,
                DefaultViewport = new ViewPortOptions { IsLandscape = true }
            };
            options.Args = new[] { "--disable-notifications" };
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
