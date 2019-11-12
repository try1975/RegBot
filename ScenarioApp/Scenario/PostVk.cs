using log4net;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;
using PuppeteerSharp.Input;
using System.Diagnostics;
using Common.Service.Interfaces;

namespace ScenarioApp.Scenario
{
    class PostVk
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GoogleSearch));
        private readonly IProgress<string> _progress;

        public PostVk(IProgress<string> progress)
        {
            _progress = progress;
        }

        private void Report(string logMessage)
        {
            Log.Info(logMessage);
            Debug.WriteLine(logMessage);
            _progress?.Report(logMessage);
        }

        public async Task RunScenario(string chromiumPath, bool headless, IAccountData accountData, string postText)
        {
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(chromiumPath, headless))
                using (var page = await browser.NewPageAsync())
                {
                    await VkAuthorization.Auth(page, accountData);
                    await page.GoToAsync($"https://vk.com/club188446341");

                    await page.WaitForSelectorAsync("div.submit_post_field", new WaitForSelectorOptions { Timeout = 5000 });
                    await page.ClickAsync("div.submit_post_field");
                    await page.WaitForTimeoutAsync(500);
                    await page.TypeAsync("div.submit_post_field", postText, new TypeOptions { Delay = 50 });
                    await page.WaitForTimeoutAsync(500);
                    await page.ClickAsync("#send_post");
                    await page.WaitForTimeoutAsync(500);

                    //await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");

                    //Report($"{text}");
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
