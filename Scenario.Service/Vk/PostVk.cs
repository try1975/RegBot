using Common.Service.Interfaces;
using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class PostVk : ScenarioBase
    {
        public PostVk(IChromiumSettings chromiumSettings, IProgress<string> progressLog = null)
            : base(typeof(PostVk), chromiumSettings, progressLog)
        {
        }

        public async Task RunScenario(IAccountData accountData, string[] vkGroups, string message)
        {
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    foreach (var vkGroup in vkGroups)
                    {
                        await VkAuthorization.Auth(page, accountData);
                        await page.GoToAsync($"https://vk.com/{vkGroup}");
                        //await page.GoToAsync($"https://vk.com/club188446341");

                        await page.WaitForSelectorAsync("div.submit_post_field", new WaitForSelectorOptions { Timeout = 5000 });
                        await page.ClickAsync("div.submit_post_field");
                        await page.WaitForTimeoutAsync(500);
                        //await page.TypeAsync("div.submit_post_field", "45", new TypeOptions { Delay = 50 });
                        await page.Keyboard.PressAsync($"{nameof(Key.Backspace)}");
                        await page.Keyboard.PressAsync($"{nameof(Key.Backspace)}");
                        await page.TypeAsync("div.submit_post_field", message, new TypeOptions { Delay = 50 });
                        await page.WaitForTimeoutAsync(500);
                        await page.ClickAsync("#send_post");
                        await page.WaitForTimeoutAsync(500);

                        //await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");

                        //Report($"{text}");
                    }
                }
            }
            catch (Exception exception)
            {
                Error(exception.ToString());
            }
        }
    }
}
