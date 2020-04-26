using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok.Bot
{
    public class OkRegistration : IBot
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(OkRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;
        //private readonly string _okProxy = System.Configuration.ConfigurationManager.AppSettings[nameof(_okProxy)];
        private static readonly TypeOptions _typeOptions = new TypeOptions { Delay = 50 };
        #endregion

        public OkRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = ServiceDomains.GetDomain(ServiceCode.Ok);
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            //_chromiumSettings.Proxy = _okProxy;
            _chromiumSettings.Proxy = _chromiumSettings.GetProxy(ServiceCode.Ok);
        }

        public async Task<IAccountData> Registration(CountryCode countryCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(await SmsServiceInit(countryCode, ServiceCode.Ok))) return _data;
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
                using (var page = await PageInit(browser)) await RegistrateByPhone(page);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                if (string.IsNullOrEmpty(_data.ErrMsg)) _data.ErrMsg = exception.Message;
                await _smsService.SetNumberFail(_requestId);
            }
            return _data;
        }

        private async Task RegistrateByPhone(Page page)
        {
            await FillPhone(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillSmsAndPassword(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillAccount(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await page.WaitForTimeoutAsync(1000);
            var eAvatar = await page.QuerySelectorAsync("div.ucard-mini");
            _data.Success = eAvatar != null;
        }

        private async Task<string> SmsServiceInit(CountryCode countryCode, ServiceCode serviceCode)
        {
            _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
            Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
            if (_smsService == null)
            {
                _data.Phone = PhoneServiceStore.GetRandomPhoneNumber(countryCode);
                return _data.ErrMsg; ;
            }
            PhoneNumberRequest phoneNumberRequest = null;
            phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, serviceCode);
            //phoneNumberRequest = new PhoneNumberRequest { Id = "444", Phone = "79163848169" };
            if (phoneNumberRequest == null)
            {
                _data.ErrMsg = BotMessages.NoPhoneNumberMessage;
                return _data.ErrMsg;
            }
            Log.Info($"phoneNumberRequest: {JsonConvert.SerializeObject(phoneNumberRequest)}");
            _requestId = phoneNumberRequest.Id;
            _data.Phone = phoneNumberRequest.Phone.Trim();
            if (!_data.Phone.StartsWith("+")) _data.Phone = $"+{_data.Phone}";
            //_data.Phone = _data.Phone.Substring(PhoneServiceStore.CountryPrefixes[countryCode].Length + 1);
            return _data.ErrMsg;
        }

        private async Task<Page> PageInit(Browser browser, bool isIncognito = false)
        {
            Page page;
            if (isIncognito)
            {
                var context = await browser.CreateIncognitoBrowserContextAsync();
                page = await context.NewPageAsync();
            }
            else page = await browser.NewPageAsync();
            #region commented
            //await SetRequestHook(page);
            //await page.EmulateAsync(Puppeteer.Devices[DeviceDescriptorName.IPhone6]); 
            #endregion
            await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
            await page.GoToAsync(GetRegistrationUrl());
            return page;
        }

        private async Task FillPhone(Page page)
        {
            await page.WaitForTimeoutAsync(100);
            for (int i = 0; i < 4; i++)
            {
                await page.Keyboard.PressAsync($"{nameof(Key.Backspace)}");
                await page.WaitForTimeoutAsync(100);
            }
            var ePhone = await page.QuerySelectorAsync("input#field_phone");
            await ePhone.TypeAsync(_data.Phone, _typeOptions);
            await page.WaitForTimeoutAsync(500);
            var eBadPhone = await page.QuerySelectorAsync("div.input-e");
            if (eBadPhone != null)
            {
                var eText = await eBadPhone.GetPropertyAsync("textContent");
                var error = $"{await eText.JsonValueAsync()}";
                if (!string.IsNullOrEmpty(error))
                {
                    _data.ErrMsg = BotMessages.PhoneNumberNotAcceptMessage;
                    return;
                }
            }
            await ClickSubmit(page);
            await page.WaitForTimeoutAsync(500);
        }

        private async Task FillSmsAndPassword(Page page)
        {
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            var eSmsLink = await page.QuerySelectorAsync("input[data-l$=sms_code]");
            if (eSmsLink != null)
            {
                await eSmsLink.ClickAsync();
                await page.WaitForTimeoutAsync(500);
            }
            var eSmsInput = await page.QuerySelectorAsync("input#smsCode");
            if (eSmsInput != null)
            {
                var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                if (phoneNumberValidation != null)
                {
                    await _smsService.SetSmsValidationSuccess(_requestId);
                    // enter sms code
                    await eSmsInput.TypeAsync(phoneNumberValidation.Code, _typeOptions);
                    await ClickSubmit(page);
                    await page.WaitForTimeoutAsync(500);
                    var ePassword = await page.QuerySelectorAsync("input#field_password");
                    await ePassword.TypeAsync(_data.Password, _typeOptions);
                    await ClickSubmit(page);
                }
                else
                {
                    _data.ErrMsg = BotMessages.PhoneNumberNotRecieveSms;
                    await _smsService.SetNumberFail(_requestId);
                }
            }
        }

        private async Task FillAccount(Page page)
        {
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            var eFirstname = await page.QuerySelectorAsync("input#field_fieldName");
            await eFirstname.TypeAsync(_data.Firstname, _typeOptions);
            var eLastname = await page.QuerySelectorAsync("input#field_surname");
            await eLastname.TypeAsync(_data.Lastname, _typeOptions);

            var eBirthday = await page.QuerySelectorAsync("input#field_birthday");
            await eBirthday.ClickAsync();

            var eYear = await page.QuerySelectorAsync("select.ui-datepicker-year");
            await eYear.ClickAsync();
            await eYear.SelectAsync($"{_data.BirthDate.Year}");

            var eMonth = await page.QuerySelectorAsync("select.ui-datepicker-month");
            await eMonth.ClickAsync();
            await eMonth.SelectAsync($"{_data.BirthDate.Month - 1}");

            var eDays = await page.QuerySelectorAllAsync("a.ui-state-default:not(.ui-priority-secondary)");
            await eDays[_data.BirthDate.Day - 1].ClickAsync();

            var eSex = await page.QuerySelectorAllAsync("span.btn-group_i_t");
            if (_data.Sex == SexCode.Male) await eSex[0].ClickAsync();
            if (_data.Sex == SexCode.Female) await eSex[1].ClickAsync();
            await page.WaitForTimeoutAsync(1000);
            await ClickSubmit(page);
        }

        private static async Task ClickSubmit(Page page)
        {
            var elSignUp = await page.QuerySelectorAsync("input[type=submit]");

            await elSignUp.ClickAsync();
            await page.WaitForTimeoutAsync(500);
        }

        public static string GetRegistrationUrl()
        {
            return @"https://ok.ru/dk?st.cmd=anonymRegistrationEnterPhone";
        }

    }
}
