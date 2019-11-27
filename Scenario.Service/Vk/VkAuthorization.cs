using Common.Service.Interfaces;
using log4net;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace ScenarioService
{
    public static class VkAuthorization
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VkAuthorization));
        public static async Task<string> Auth(Page page, IAccountData accountData)
        {
            if (accountData == null) return string.Empty;
            try
            {
                await page.GoToAsync($"https://vk.com");
                var loginSelectorVkAuthorization = "input#index_email";
                await page.ClickAsync(loginSelectorVkAuthorization);
                await page.TypeAsync(loginSelectorVkAuthorization, accountData.Phone);
                var passwordSelectorVkAuthorization = "input#index_pass";
                await page.ClickAsync(passwordSelectorVkAuthorization);
                await page.TypeAsync(passwordSelectorVkAuthorization, accountData.Password);
                await page.ClickAsync("div#index_expire");
                await page.ClickAsync("button#index_login_button");
                await page.WaitForNavigationAsync();
                if (await page.QuerySelectorAsync("div.top_profile_name") == null) return string.Empty;
                if (page.Url.ToLower().Contains("blocked")) return string.Empty;
                await page.ClickAsync("li#l_pr");
                await page.WaitForNavigationAsync();
                return page.Url;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return string.Empty;
            }
        }
    }
}
