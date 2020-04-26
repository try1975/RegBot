using Common.Service.Interfaces;
using log4net;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace ScenarioService
{
    public static class FbAuthorization
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FbAuthorization));
        public static async Task<string> Auth(Page page, IAccountData accountData)
        {
            if (accountData == null) return string.Empty;
            try
            {
                await page.GoToAsync($"https://www.facebook.com/");
                var selectorLogin = "input[name='email']";
                await page.ClickAsync(selectorLogin);
                await page.TypeAsync(selectorLogin, accountData.Phone);
                var selectorPassword = "input[name='pass']";
                await page.ClickAsync(selectorPassword);
                await page.TypeAsync(selectorPassword, accountData.Password);
                if (await page.QuerySelectorAsync("input[type='submit']") != null) await page.ClickAsync("input[type='submit']");
                if (await page.QuerySelectorAsync("button[name='login']") != null) await page.ClickAsync("button[name='login']");
                var response = await page.WaitForNavigationAsync();
                var profile = await page.QuerySelectorAsync("a[title = 'Profile']");
                if (profile == null) return string.Empty;
                var text = (await (await profile.GetPropertyAsync("href")).JsonValueAsync()).ToString();
                return text;
                //return page.Url;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return string.Empty;
            }
        }
    }
}
