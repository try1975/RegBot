﻿using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class YandexSearch : ScenarioBase
    {
        public YandexSearch(IChromiumSettings chromiumSettings, IProgress<string> progressLog = null)
            : base(typeof(YandexSearch), chromiumSettings, progressLog)
        {
        }

        public async Task<List<string>> RunScenario(string[] queries, int pageCount)
        {
            var result = new List<string>();
            try
            {
                var navigationOptions = new NavigationOptions { Timeout = 60000 };
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
                using (var page = await browser.NewPageAsync())
                {
                    await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
                    var linkCount = 1;
                    foreach (var query in queries)
                    {
                        await page.GoToAsync($"https://yandex.ru", navigationOptions);

                        await page.TypeAsync("input", query, new TypeOptions { Delay = 150 });
                        await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");
                        await page.WaitForNavigationAsync(navigationOptions);

                        var contentSelector = "div.organic>h2>a";
                        for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                        {
                            var links = await page.QuerySelectorAllAsync(contentSelector);
                            foreach (var item in links)
                            {
                                var text = (await (await item.GetPropertyAsync("href")).JsonValueAsync()).ToString();
                                if (text.Contains("http") && !text.Contains("yabs.yandex"))
                                {
                                    Report($"------------{linkCount}------------------------------------------------");
                                    Report($"{text}");
                                    result.Add(text);
                                    linkCount++;
                                }
                            }
                            if (pageIndex < pageCount - 1)
                            {
                                await page.ClickAsync("div.pager a:last-child");
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
