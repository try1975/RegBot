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
            var extensionWebRTCPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-706915\\chrome-win\\", "extensions\\webrtc");
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-706915\\chrome-win\\chrome.exe");

            //chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            var options = new LaunchOptions
            {
                Headless = headless,
                ExecutablePath = chromiumPath,
                DefaultViewport = new ViewPortOptions { IsLandscape = true },
                //DefaultViewport = new ViewPortOptions { IsLandscape = false, IsMobile= true },
                IgnoreHTTPSErrors = true,
                SlowMo = 10
            };

            //var connectOptions = new ConnectOptions { BrowserWSEndpoint = $"wss://chrome.browserless.io?token={apikey}", BrowserURL="http://127.0.0.1:2122" };
            //await Puppeteer.ConnectAsync(connectOptions);
            //var connectOptions = new ConnectOptions { BrowserURL="http://127.0.0.1:9222" };
            //return await Puppeteer.ConnectAsync(connectOptions);

            var optionsArgs = new List<string>
            {
            //    "--disable-background-timer-throttling",
            //    "--disable-breakpad",
            //    "--disable-client-side-phishing-detection",
            //    "--disable-cloud-import",
            //    "--disable-default-apps",
            //    "--disable-dev-shm-usage",
            //    "--disable-extensions",
            //    "--disable-gesture-typing",
            //    "--disable-hang-monitor",
            //    "--disable-infobars",
            //    "--disable-notifications",
            //    "--disable-offer-store-unmasked-wallet-cards",
            //    "--disable-offer-upload-credit-cards",
            //    "--disable-popup-blocking",
            //    "--disable-print-preview",
            //    "--disable-prompt-on-repost",
            //    "--disable-setuid-sandbox",
            //    "--disable-speech-api",
            //    "--disable-sync",
            //    "--disable-tab-for-desktop-share",
            //    "--disable-translate",
            //    "--disable-voice-input",
            //    "--disable-wake-on-wifi",
            //    "--disable-web-security",
            //    "--disk-cache-size=33554432",
            //    "--enable-async-dns",
            //    "--enable-simple-cache-backend",
            //    "--enable-tcp-fast-open",
            //    "--enable-webgl",
            //    "--hide-scrollbars",
            //    "--ignore-certifcate-errors",
            //    "--ignore-certifcate-errors-spki-list",
            //    "--ignore-gpu-blacklist",
            //    "--media-cache-size=33554432",
            //    "--metrics-recording-only",
            //    "--mute-audio",
            //    "--no-default-browser-check",
            //    "--no-first-run",
            //    "--no-pings",
            //    "--no-sandbox",
            //    "--no-zygote",
            //    "--password-store=basic",
            //    "--prerender-from-omnibox=disabled",
            //    "--use-gl=swiftshader",
            //    "--use-mock-keychain",
            //    "--window-position=100,0"

                "--disable-notifications"
                , "--no-sandbox"
                , "--disable-setuid-sandbox"
                , "--disable-infobars"
                , "--window-position=0,0"
                , "--ignore-certifcate-errors"
                , "--ignore-certifcate-errors-spki-list"
                , "--lang=bn-BD,bn"
            };
            if (args != null) optionsArgs.AddRange(args);

            // webRTC try disable
            if (Directory.Exists(extensionWebRTCPath))
            {
                optionsArgs.Add($"--disable-extensions-except={extensionWebRTCPath}");
                optionsArgs.Add($"--load-extension=={extensionWebRTCPath}");
            }

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
