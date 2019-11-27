using Common.Service.Interfaces;
using PuppeteerService;
using PuppeteerSharp.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class CollectVkWall : ScenarioBase
    {
        public CollectVkWall(IChromiumSettings chromiumSettings, IProgress<string> progressLog = null)
            : base(typeof(CollectVkWall), chromiumSettings, progressLog)
        {
        }

        public async Task<List<string>> RunScenario(IAccountData accountData, string[] vkAccountNames, int pageCount)
        {
            var result = new List<string>();
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    await VkAuthorization.Auth(page, accountData);
                    var i = 1;
                    foreach (var vkAccountName in vkAccountNames)
                    {
                        await page.GoToAsync($"https://vk.com/{vkAccountName}");

                        var postContentSelector = "div.post_content";
                        var postContent = await page.QuerySelectorAllAsync(postContentSelector);
                        var postCount = postContent.Length;

                        var cycleIndex = 1;
                        var postCountPrev = 0;
                        while (postCount >= postCountPrev)
                        {
                            if (cycleIndex >= pageCount) break;
                            postCountPrev = postCount;
                            await page.Keyboard.DownAsync($"{nameof(Key.Control)}");
                            await page.Keyboard.PressAsync($"{nameof(Key.End)}");
                            await page.Keyboard.UpAsync($"{nameof(Key.Control)}");
                            await page.WaitForTimeoutAsync(300);
                            postContent = await page.QuerySelectorAllAsync(postContentSelector);
                            postCount = postContent.Length;
                            cycleIndex++;
                        }

                        foreach (var item in postContent)
                        {
                            Report($"------------{i}------------------------------------------------");
                            var text = (await (await item.GetPropertyAsync("innerText")).JsonValueAsync()).ToString();
                            Report(text);
                            result.Add(text);
                            i++;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Error(exception.ToString());
            }
            return result;
        }
    }
}
