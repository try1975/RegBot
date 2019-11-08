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
    class VkWall
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VkWall));
        private readonly string _chromiumPath;
        private readonly IProgress<string> _progress;

        public VkWall(string chromiumPath, IProgress<string> progress)
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

        public async Task Registration(string vkAccountName, int vkPageCount, bool headless)
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
                    await page.GoToAsync($"https://vk.com/{vkAccountName}");


                    var postContentSelector = "div.post_content";
                    var postContent = await page.QuerySelectorAllAsync(postContentSelector);
                    var postCount = postContent.Length;

                    var cycleIndex = 1;
                    var postCountPrev = 0;
                    while (postCount >= postCountPrev)
                    {
                        if (cycleIndex >= vkPageCount) break;
                        postCountPrev = postCount;
                        await page.Keyboard.DownAsync($"{nameof(Key.Control)}");
                        await page.Keyboard.PressAsync($"{nameof(Key.End)}");
                        await page.Keyboard.UpAsync($"{nameof(Key.Control)}");
                        await page.WaitForTimeoutAsync(300);
                        postContent = await page.QuerySelectorAllAsync(postContentSelector);
                        postCount = postContent.Length;
                        cycleIndex++;
                    }
                    var i = 1;
                    foreach (var item in postContent)
                    {
                        Report($"------------{i}------------------------------------------------");
                        Report((await (await item.GetPropertyAsync("innerText")).JsonValueAsync()).ToString());
                        i++;
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
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
