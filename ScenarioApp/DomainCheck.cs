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
    class DomainCheck
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GoogleSearch));
        private readonly string _chromiumPath;
        private readonly IProgress<string> _progress;

        public DomainCheck(string chromiumPath, IProgress<string> progress)
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

        public async Task Registration(string domainName, bool headless)
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
                    await page.GoToAsync($"https://www.nic.ru/whois");

                    await page.TypeAsync("input", domainName, new TypeOptions { Delay = 50 });
                    await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");
                    //await page.WaitForTimeoutAsync(1500);

                    var contentSelector = "div[data-qa='Whois-card']";
                    var data = await page.WaitForSelectorAsync(contentSelector, new WaitForSelectorOptions { Timeout = 3000 });
                    var text = (await (await data.GetPropertyAsync("innerText")).JsonValueAsync()).ToString();
                    text = text.Remove(text.IndexOf("<<<"));
                    Report($"{text}");
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _progress?.Report(exception.Message);
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
