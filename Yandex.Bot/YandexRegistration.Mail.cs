using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace Yandex.Bot
{
    public partial class YandexRegistration
    {
        public static string GetLoginUrl()
        {
            return @"https://passport.yandex.ru/auth";
        }

        public static async Task<bool> EmailAlreadyRegistered(string accountName, Page page)
        {
            var eLogin = await page.QuerySelectorAsync("input[name=login]");
            await eLogin.TypeAsync(accountName, _typeOptions);
            await page.WaitForTimeoutAsync(1000);
            var altMailExists = await page.QuerySelectorAsync("div[data-t='login-error']");
            return altMailExists == null ? false : true;
        }

        public static async Task<bool> Login(string accountName, string password, Page page)
        {
            try
            {
                await page.TypeAsync("input[name=login]", accountName);
                await page.WaitForTimeoutAsync(500);
                await page.ClickAsync("button[type=submit]");
                //await page.WaitForNavigationAsync();
                await page.WaitForTimeoutAsync(500);
                await page.TypeAsync("input[type=password]", password);
                await page.ClickAsync("button[type=submit]");
                var navigationOptions = new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded } };
                await page.WaitForNavigationAsync(navigationOptions);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return false;
            }
            return true;
        }

        public static async Task<bool> SendEmail(string to, string subject, string[] text, Page page)
        {
            try
            {
                await page.GoToAsync("https://mail.yandex.ru/");
                var selNewLetter = "span.mail-ComposeButton-Text";
                await page.WaitForSelectorAsync(selNewLetter);
                await page.ClickAsync(selNewLetter);
                await page.WaitForTimeoutAsync(1500);
                var selTo = "div[name=to]";
                await page.ClickAsync(selTo);
                await page.TypeAsync(selTo, to, _typeOptions);
                var selSubject = "input[name ^= subj]";
                await page.ClickAsync(selSubject);
                await page.TypeAsync(selSubject, subject, _typeOptions);
                var selText = "div[role=textbox]";
                await page.ClickAsync(selText);
                await page.TypeAsync(selText, string.Join(Environment.NewLine, text), _typeOptions);
                // or CTRL+ENTER 
                await page.ClickAsync("button[type=submit]");

                await page.WaitForNavigationAsync();

            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return false;
            }
            return true;
        }
    }
}
