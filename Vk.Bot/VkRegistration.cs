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
    public class VkRegistration : IBot
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(VkRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;

        public VkRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = "vk.com";
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
        }

        public async Task<IAccountData> Registration(CountryCode countryCode)
        {
            try
            {
                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, ServiceCode.Vk);
                //var phoneNumberRequest = new PhoneNumberRequest { Id = "444", Phone = "79777197334" };
                if (phoneNumberRequest == null)
                {
                    _data.ErrMsg = BotMessages.NoPhoneNumberMessage;
                    return _data;
                }
                Log.Info($"phoneNumberRequest: {JsonConvert.SerializeObject(phoneNumberRequest)}");
                _requestId = phoneNumberRequest.Id;
                _data.Phone = phoneNumberRequest.Phone.Trim();
                if (!_data.Phone.StartsWith("+")) _data.Phone = $"+{_data.Phone}";

                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    try
                    {
                        await FillRegistrationData(page);

                        await page.WaitForSelectorAsync("div#join_country_row");
                        //select country
                        //await page.ClickAsync("div#join_country_row td#dropdown1");
                        await page.ClickAsync("div#join_country_row td.selector_dropdown");
                        var countryPrefix = PhoneServiceStore.CountryPrefixes[countryCode];
                        await page.WaitForTimeoutAsync(1000);
                        await page.ClickAsync($"div#join_country_row li[title*='+{countryPrefix}']");

                        await page.TypeAsync("input#join_phone", _data.Phone.Substring(countryPrefix.Length + 1));
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
                            return _data;
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
                        await page.WaitForSelectorAsync(selSmsCode, new WaitForSelectorOptions { Visible = true });
                        await page.ClickAsync(selSmsCode);
                        var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                        Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                        if (phoneNumberValidation != null)
                        {
                            await _smsService.SetSmsValidationSuccess(_requestId);
                            await page.TypeAsync(selSmsCode, phoneNumberValidation.Code);
                            await page.ClickAsync("button#join_send_code");
                            await page.WaitForSelectorAsync("input#join_pass", new WaitForSelectorOptions { Visible = true });
                            await page.TypeAsync("input#join_pass", _data.Password);
                            await page.ClickAsync("button#join_send_pass");
                            await page.WaitForNavigationAsync();
                            await page.ClickAsync("a.join_skip_link");
                            await page.WaitForTimeoutAsync(1000);
                            _data.Success = true;
                        }
                        else await _smsService.SetNumberFail(_requestId);
                    }
                    catch (Exception exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _data.ErrMsg = exception.Message;
                await _smsService.SetNumberFail(_requestId);
            }

            return _data;
        }

        private async Task FillRegistrationData(Page page)
        {
            await page.GoToAsync("https://vk.com/join");

            #region Name
            await page.ClickAsync("input#ij_first_name");
            await page.TypeAsync("input#ij_first_name", _data.Firstname);
            await page.TypeAsync("input#ij_last_name", _data.Lastname);

            #endregion
            await page.ClickAsync("div.ij_bday td#dropdown1");
            await page.ClickAsync($"div.ij_bday li[val = '{_data.BirthDate.Day}']");
            await page.ClickAsync("div.ij_bmonth td#dropdown2");
            await page.ClickAsync($"div.ij_bmonth li[val = '{_data.BirthDate.Month}']");
            await page.ClickAsync("div.ij_byear td#dropdown3");
            await page.ClickAsync($"div.ij_byear li[val = '{_data.BirthDate.Year}']");

            await page.ClickAsync("button#ij_submit");

            await page.WaitForSelectorAsync("div#ij_sex_row", new WaitForSelectorOptions { Visible = true });
            if (_data.Sex == SexCode.Female) await page.ClickAsync("div#ij_sex_row > div[tabindex='0']");
            if (_data.Sex == SexCode.Male) await page.ClickAsync("div#ij_sex_row > div[tabindex='-1']");

            await page.ClickAsync("button#ij_submit");
        }

    }
}
