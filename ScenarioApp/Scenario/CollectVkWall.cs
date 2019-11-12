using log4net;
using System;
using System.Threading.Tasks;
using PuppeteerSharp.Input;
using System.Diagnostics;
using Common.Service.Interfaces;

namespace ScenarioApp
{
    class CollectVkWall
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CollectVkWall));
        private readonly IProgress<string> _progress;
        private readonly IAccountData _accountData;

        public CollectVkWall(IProgress<string> progress, IAccountData accountData)
        {
            _progress = progress;
            _accountData = accountData;
        }

        private void Report(string logMessage)
        {
            Log.Info(logMessage);
            Debug.WriteLine(logMessage);
            _progress?.Report(logMessage);
        }

        public async Task RunScenario(string chromiumPath, bool headless, string vkAccountName, int vkPageCount)
        {
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(chromiumPath, headless))
                using (var page = await browser.NewPageAsync())
                {
                    await VkAuthorization.Auth(page, _accountData);

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
    }
}
