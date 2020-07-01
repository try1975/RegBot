using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class HunterSearch : ScenarioBase
    {
        private readonly IProgress<string> _progressResult;

        public HunterSearch(IChromiumSettings chromiumSettings, IProgress<string> progressLog = null, IProgress<string> progressResult = null)
            : base(typeof(GoogleSearch), chromiumSettings, progressLog)
        {
            _progressResult = progressResult;
        }

        public async Task<List<string>> RunScenario(string[] queries, int pageCount)
        {
            var result = new List<string>();
            try
            {
                
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
                using (var page = await browser.NewPageAsync())
                {
                    var navigationOptions = new NavigationOptions { Timeout = 60000, WaitUntil = new[] { WaitUntilNavigation.Networkidle0 } };
                    await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
                    foreach (var query in queries)
                    {
                        await page.GoToAsync($"https://hunter.ddosecrets.com/search", navigationOptions);
                        //await page.WaitForNavigationAsync(navigationOptions);

                        await page.TypeAsync("#search-box", query, new TypeOptions { Delay = 50 });
                        await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");
                        //await page.WaitForTimeoutAsync(5000);

                        var contentSelector = "a.EntityLink";
                        await page.WaitForSelectorAsync(contentSelector);
                        var elementHandles = await page.QuerySelectorAllAsync(contentSelector);
                        var elementCount = elementHandles.Length;
                        await page.ClickAsync("main.ContentPane");

                        var elementCountPrev = 0;
                        while (elementCount > elementCountPrev)
                        {
                            elementCountPrev = elementCount;
                            await page.Keyboard.DownAsync($"{nameof(Key.Control)}");
                            await page.Keyboard.PressAsync($"{nameof(Key.End)}");
                            await page.Keyboard.UpAsync($"{nameof(Key.Control)}");
                            await page.WaitForTimeoutAsync(1500);
                            elementHandles = await page.QuerySelectorAllAsync(contentSelector);
                            elementCount = elementHandles.Length;
                        }
                        Report($"query:{query} - found {elementCount} links");
                        var downloadLinks = new List<string>(elementCount);
                        foreach (var item in elementHandles)
                        {
                            var text = (await (await item.GetPropertyAsync("href")).JsonValueAsync()).ToString();
                            downloadLinks.Add(text);
                        }
                        navigationOptions = new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded } };
                        foreach (var link in downloadLinks)
                        {
                            await page.GoToAsync(link, navigationOptions);
                            ElementHandle fileLink;
                            try
                            {
                                fileLink = await page.WaitForSelectorAsync("a.DownloadButton");
                            }
                            catch
                            {
                                await page.ClickAsync("li span a.EntityLink");
                                fileLink = await page.WaitForSelectorAsync("a.DownloadButton");
                            }
                            var href = (await (await fileLink.GetPropertyAsync("href")).JsonValueAsync()).ToString();
                           
                            _progressResult?.Report(href);
                            result.Add(href);
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
