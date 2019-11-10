using System;
using System.Threading.Tasks;
using Common.Service.Interfaces;
using log4net;
using PuppeteerSharp;

namespace ScenarioApp
{
    static class VkAuthorization
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VkAuthorization));
        public static async Task Auth(Page page, IAccountData accountData)
        {
            if (accountData == null) return;
            try
            {
                await page.GoToAsync($"https://vk.com");
                await page.ClickAsync("input#index_email");
                await page.TypeAsync("input#index_email", accountData.Phone);
                await page.ClickAsync("input#index_pass");
                await page.TypeAsync("input#index_pass", accountData.Password);
                await page.ClickAsync("div#index_expire");
                await page.ClickAsync("button#index_login_button");
                await page.WaitForNavigationAsync();
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
        }
    }
}
