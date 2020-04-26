using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace MailRu.Bot
{
    public partial class MailRuRegistration
    {
        public static string GetLoginUrl()
        {
            return @"https://account.mail.ru/login";
        }

        public static async Task<bool> EmailAlreadyRegistered(string accountName, string host, Page page)
        {
            var eAccountName = await page.QuerySelectorAsync("span.b-email__name>input[type='email']");
            if (eAccountName == null) eAccountName = await page.QuerySelectorAsync("input[data-test-id='account__input']");
            await eAccountName.TypeAsync(accountName);

            const string defaultDomain = "mail.ru";
            if (string.IsNullOrEmpty(host)) host = defaultDomain;

            if (!host.ToLower().Equals(defaultDomain))
            {
                //select domain
                var eDomain = await page.QuerySelectorAsync("span.b-email__domain span");
                if (eDomain == null) eDomain = await page.QuerySelectorAsync("div[data-test-id='account__select']");
                await eDomain.ClickAsync();
                var eDomainValue = await page.QuerySelectorAsync($"a[data-text='@{host}']");
                if (eDomainValue == null) eDomainValue = await page.QuerySelectorAsync($"div[data-test-id='select-value:@{host}']");
                await eDomainValue.ClickAsync();
            }

            await page.WaitForTimeoutAsync(1000);
            var altMailExists = await page.QuerySelectorAsync("div.b-tooltip_animate");
            if (altMailExists == null) altMailExists = await page.QuerySelectorAsync("[data-test-id='exists']");
            return altMailExists != null;
        }

        public static async Task<bool> Login(string accountName, string password, Page page)
        {
            //page.EmulateAsync(DeviceDescriptors.Get(DeviceDescriptorName.IPhone6);
            try
            {
                await page.TypeAsync("input[name=Login]", accountName, _typeOptions);
                await page.WaitForTimeoutAsync(500);
                await ClickSubmit(page);
                await page.WaitForTimeoutAsync(500);
                await page.TypeAsync("input[name=Password]", password, _typeOptions);
                await ClickSubmit(page);
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
                await page.WaitForTimeoutAsync(2000);
                var selNewLetter = "span.compose-button>span>span";
                if (await page.QuerySelectorAsync(selNewLetter) == null) selNewLetter = "a[data-name=compose] span";
                await page.ClickAsync(selNewLetter);
                await page.WaitForTimeoutAsync(1500);
                var selTo = "div[data-type=to] input";
                if (await page.QuerySelectorAsync(selTo) == null) selTo = "div[data-blockid='compose_to']";
                await page.ClickAsync(selTo);
                await page.TypeAsync(selTo, to, _typeOptions);

                var selSubject = "input[name=Subject]";
                //await page.ClickAsync("label[data-for=Subject]") ;
                await page.TypeAsync(selSubject, subject, _typeOptions);
                var selText = "div[role=textbox] div div";
                if (await page.QuerySelectorAsync(selText) == null)
                {
                    var elText = await page.QuerySelectorAsync("span.mceEditor iframe");
                    var frame = await elText.ContentFrameAsync();
                    var elBody = await frame.QuerySelectorAsync("body");
                    await elBody.TypeAsync(string.Join(Environment.NewLine, text), _typeOptions);
                }
                else
                {
                    await page.ClickAsync(selText);
                    await page.TypeAsync(selText, string.Join(Environment.NewLine, text), _typeOptions);
                }
                // or CTRL+ENTER 

                var selSend = "span[data-title-shortcut='Ctrl+Enter']";
                if (await page.QuerySelectorAsync(selSend) == null) selSend = "div[data-name=send]";
                await page.ClickAsync(selSend);
                await page.WaitForNavigationAsync(new NavigationOptions { Timeout = 5000 });

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
