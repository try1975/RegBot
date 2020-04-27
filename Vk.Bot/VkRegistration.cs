using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace Vk.Bot
{
    public class VkRegistration : RegistrationBot.Bot
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(VkRegistration));
        public static readonly string RegistartionUrl = "https://vk.com/join";

        public VkRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings) : base(data, smsService, chromiumSettings)
        {
            _chromiumSettings.Proxy = _chromiumSettings.GetProxy(ServiceCode.Vk);
        }

        protected override ServiceCode GetServiceCode() => ServiceCode.Vk;

        protected override string GetRegistrationUrl() => RegistartionUrl;

        protected override async Task StartRegistration(Page page)
        {
            await RegistrateByPhone(page);
        }

        private async Task RegistrateByPhone(Page page)
        {
            await FillRegistrationData(page);
            await page.WaitForTimeoutAsync(1000);
            await page.WaitForSelectorAsync("div#join_country_row");
            //select country
            //await page.ClickAsync("div#join_country_row td#dropdown1");
            await page.ClickAsync("div#join_country_row td.selector_dropdown");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync($"div#join_country_row li[title*='(+{_countryPrefix})']");

            await page.TypeAsync("input#join_phone", _data.Phone.Substring(_countryPrefix.Length + 1), _typeOptions);
            await page.ClickAsync("div#join_accept_terms_checkbox div.checkbox");

            await page.ClickAsync("div#join_phone_submit button");

            // check phone accepted
            var selResend = "div#join_resend";
            await page.WaitForTimeoutAsync(1000);
            var elResend = await page.QuerySelectorAsync(selResend);
            if (!await elResend.IsIntersectingViewportAsync())
            {
                await _smsService.SetNumberFail(_requestId);
                _data.ErrMsg = BotMessages.PhoneNumberNotAcceptMessage;
                return;
            }

            var selCalledPhone = "input#join_called_phone";
            var elCalledPhone = await page.QuerySelectorAsync(selCalledPhone);
            if (elCalledPhone != null && await elCalledPhone.IsIntersectingViewportAsync())
            {
                var selSmsSend = "div#join_resend a#join_resend_lnk";
                await page.WaitForSelectorAsync(selSmsSend, new WaitForSelectorOptions { Timeout = 120000 });
                await page.ClickAsync(selSmsSend);
            }

            var selSmsCode = "input#join_code";
            await page.WaitForSelectorAsync(selSmsCode, new WaitForSelectorOptions { Visible = true, Timeout = 60000 });
            await page.ClickAsync(selSmsCode);
            var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
            Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
            if (phoneNumberValidation != null)
            {
                await _smsService.SetSmsValidationSuccess(_requestId);
                await page.TypeAsync(selSmsCode, phoneNumberValidation.Code, _typeOptions);
                await page.ClickAsync("button#join_send_code");
                await page.WaitForSelectorAsync("input#join_pass", new WaitForSelectorOptions { Visible = true });
                await page.TypeAsync("input#join_pass", _data.Password, _typeOptions);
                await page.ClickAsync("button#join_send_pass");
                await page.WaitForNavigationAsync();
                await page.ClickAsync("a.join_skip_link");
                await page.WaitForTimeoutAsync(1000);
                _data.Success = true;
            }
            else await _smsService.SetNumberFail(_requestId);
        }

        private async Task FillRegistrationData(Page page)
        {
            #region Name
            await page.ClickAsync("input#ij_first_name");
            await page.TypeAsync("input#ij_first_name", _data.Firstname, _typeOptions);
            await page.TypeAsync("input#ij_last_name", _data.Lastname, _typeOptions);

            #endregion
            await page.ClickAsync("div.ij_bday td#dropdown1");
            await page.ClickAsync($"div.ij_bday li[val = '{_data.BirthDate.Day}']");
            await page.ClickAsync("div.ij_bmonth td#dropdown2");
            await page.ClickAsync($"div.ij_bmonth li[val = '{_data.BirthDate.Month}']");
            await page.ClickAsync("div.ij_byear td#dropdown3");
            await page.ClickAsync($"div.ij_byear li[val = '{_data.BirthDate.Year}']");

            //var sex = await page.QuerySelectorAsync("div#ij_sex_row");
            //if (sex != null)
            //{
            //    await page.ClickAsync("button#ij_submit");
            //    await page.WaitForTimeoutAsync(500);
            //}
            var sex = await page.QuerySelectorAsync("div#ij_sex_row");
            if (sex != null)
            {
                await page.WaitForSelectorAsync("div#ij_sex_row", new WaitForSelectorOptions { Visible = true });
                if (_data.Sex == SexCode.Female) await page.ClickAsync("div#ij_sex_row > div[tabindex='0']");
                if (_data.Sex == SexCode.Male) await page.ClickAsync("div#ij_sex_row > div[tabindex='-1']");
            }
            var eSubmitButton = await page.QuerySelectorAsync("button#ij_submit");
            if (eSubmitButton != null) await eSubmitButton.ClickAsync();
        }
    }
}
