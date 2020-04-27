using AccountData.Service;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace Gmail.Bot
{
    public partial class GmailRegistration
    {
        public async static Task<bool> EmailAlreadyRegistered(string accountName, Page page, string path = null)
        {
            try
            {
                if (path != null)
                {
                    var _data = new AccountDataGenerator(path).GetRandom();
                    await page.TypeAsync("input[name=firstName]", _data.Firstname);
                    await page.TypeAsync("input[name=lastName]", _data.Lastname);

                    await page.TypeAsync("input[name=Passwd]", _data.Password);
                    await page.TypeAsync("input[name=ConfirmPasswd]", _data.Password);
                }

                const string selLogin = "input[name=Username]";
                await page.ClickAsync(selLogin);
                await page.EvaluateFunctionAsync("function() {" + $"document.querySelector('{selLogin}').value = ''" + "}");
                await page.TypeAsync(selLogin, accountName);

                await page.ClickAsync("div#accountDetailsNext span>span");

                await page.WaitForTimeoutAsync(2000);
                const string selAltEmail = "ul#usernameList li";
                var elAltEmail = await page.QuerySelectorAsync(selAltEmail);
                if (!(elAltEmail != null && await elAltEmail.IsIntersectingViewportAsync())) return false;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            return true;
        }
    }
}
