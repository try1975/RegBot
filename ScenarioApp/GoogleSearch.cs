using log4net;
using PuppeteerSharp;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using PuppeteerSharp.Input;
using System.Diagnostics;

namespace ScenarioApp
{
    class GoogleSearch
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GoogleSearch));
        private readonly string _chromiumPath;
        private readonly IProgress<string> _progress;

        public GoogleSearch(string chromiumPath, IProgress<string> progress)
        {
            if (string.IsNullOrEmpty(chromiumPath)) chromiumPath = Environment.CurrentDirectory;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            _chromiumPath = chromiumPath;
            _progress = progress;
        }

        private void Report(string logMessage)
        {
            Log.Info(logMessage);
            Debug.WriteLine(logMessage);
            _progress?.Report(logMessage);
        }

        public async Task Registration(string googleQuery, int googlePageCount, bool headless)
        {
            try
            {
                var options = new LaunchOptions
                {
                    Headless = headless,
                    ExecutablePath = _chromiumPath,
                    DefaultViewport = new ViewPortOptions { IsLandscape = true }
                };
                options.Args = new[] { "--disable-notifications" };


                if (Environment.OSVersion.VersionString.Contains("NT 6.1")) { options.WebSocketFactory = WebSocketFactory; }

                using (var browser = await Puppeteer.LaunchAsync(options))
                using (var page = await browser.NewPageAsync())
                {
                    await page.GoToAsync($"https://google.ru");

                    await page.TypeAsync("input", googleQuery, new TypeOptions { Delay = 150 });
                    await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");
                    await page.WaitForNavigationAsync();

                    var linkCount = 1;
                    var contentSelector = "div.r>a";
                    for (int pageIndex = 0; pageIndex < googlePageCount; pageIndex++)
                    {
                        var links = await page.QuerySelectorAllAsync(contentSelector);
                        foreach (var item in links)
                        {
                            Report($"------------{linkCount}------------------------------------------------");
                            Report((await (await item.GetPropertyAsync("href")).JsonValueAsync()).ToString());
                            linkCount++;
                        }
                        if (pageIndex < googlePageCount - 1)
                        {
                            await page.ClickAsync("#pnnext > :nth-child(2)");
                            await page.WaitForNavigationAsync();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _progress?.Report(exception.ToString());
            }
        }

        private static async Task<WebSocket> WebSocketFactory(Uri url, IConnectionOptions options,
            CancellationToken cancellationToken)
        {
            var ws = new System.Net.WebSockets.Managed.ClientWebSocket();
            await ws.ConnectAsync(url, cancellationToken);
            return ws;
        }
    }
}
