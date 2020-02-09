using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class GoogleSearch : ScenarioBase
    {
        public GoogleSearch(IChromiumSettings chromiumSettings, IProgress<string> progressLog = null)
            : base(typeof(GoogleSearch), chromiumSettings, progressLog)
        {
        }

        public async Task<List<string>> RunScenario(string[] queries, int pageCount)
        {
            var result = new List<string>();
            try
            {
                //прокси
                List<string> args = null;
                if (!string.IsNullOrEmpty(_chromiumSettings.Proxy))
                {
                    var proxy = _chromiumSettings.Proxy.Split('@')[1];
                    args = new List<string> { $"--proxy-server={proxy}" };
                }
                var navigationOptions = new NavigationOptions { Timeout = 60000 };
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), args))
                using (var page = await browser.NewPageAsync())
                {
                    //авторизация прокси
                    if (!string.IsNullOrEmpty(_chromiumSettings.Proxy))
                    {
                        var credentials = _chromiumSettings.Proxy.Split('@')[0];
                        var userAndPassword = credentials.Split(':');
                        var username = userAndPassword[0];
                        var password = userAndPassword[1];
                        await page.AuthenticateAsync(new Credentials { Username = username, Password = password });
                    }
                    var linkCount = 1;
                    foreach (var query in queries)
                    {
                        await page.GoToAsync($"https://google.ru", navigationOptions);

                        await page.TypeAsync("input", query, new TypeOptions { Delay = 150 });
                        await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");
                        await page.WaitForNavigationAsync(navigationOptions);

                        var contentSelector = "div.r>a";
                        for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                        {
                            var links = await page.QuerySelectorAllAsync(contentSelector);
                            foreach (var item in links)
                            {
                                Report($"------------{linkCount}------------------------------------------------");
                                var text = (await (await item.GetPropertyAsync("href")).JsonValueAsync()).ToString();
                                Report(text);
                                result.Add(text);
                                linkCount++;
                            }
                            if (pageIndex < pageCount - 1)
                            {
                                await page.ClickAsync("#pnnext > :nth-child(2)");
                                await page.WaitForNavigationAsync(navigationOptions);
                            }
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
