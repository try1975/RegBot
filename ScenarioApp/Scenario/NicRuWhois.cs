using log4net;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;
using PuppeteerSharp.Input;
using System.Diagnostics;

namespace ScenarioApp
{
    class NicRuWhois
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GoogleSearch));
        private readonly IProgress<string> _progress;

        public NicRuWhois( IProgress<string> progress)
        {
            _progress = progress;
        }

        private void Report(string logMessage)
        {
            Log.Info(logMessage);
            Debug.WriteLine(logMessage);
            _progress?.Report(logMessage);
        }

        public async Task RunScenario(string chromiumPath, bool headless, string domainName)
        {
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(chromiumPath, headless))
                using (var page = await browser.NewPageAsync())
                {
                    await page.GoToAsync($"https://www.nic.ru/whois");

                    await page.TypeAsync("input", domainName, new TypeOptions { Delay = 50 });
                    await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");

                    var contentSelector = "div[data-qa='Whois-card']";
                    var data = await page.WaitForSelectorAsync(contentSelector, new WaitForSelectorOptions { Timeout = 5000 });
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
    }
}
