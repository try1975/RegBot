using Common.Service;
using Common.Service.Enums;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.Bot
{
    public partial class YandexRegistration
    {
        #region Init
        private async Task<string> SmsServiceInit(CountryCode countryCode, ServiceCode serviceCode)
        {
            _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
            Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
            if (_smsService == null)
            {
                _data.Phone = "79163848169";
                //TODO: random phone by country code
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
            await SetUserAgent(page);
            //await page.EmulateAsync(Puppeteer.Devices[DeviceDescriptorName.IPhone6]); 
            #endregion
            await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
            await page.GoToAsync(GetRegistrationUrl());
            return page;
        }

        public static string GetRegistrationUrl()
        {
            return @"https://passport.yandex.ru/registration/mail?from=mail&origin=home_desktop_ru&retpath=https%3A%2F%2Fmail.yandex.ru%2F";
            //https://passport.yandex.ru/auth/reg?from=mail&origin=home_desktop_ru&retpath=https%3A%2F%2Fmail.yandex.ru%2F
        }

        private async Task SetUserAgent(Page page)
        {
            //var userAgent = UserAgent.GetRandomUserAgent();
            var userAgent = _chromiumSettings.GetUserAgent();
            //userAgent = "Opera/9.80 (Windows NT 6.1; U; en-GB) Presto/2.7.62 Version/11.00";
            //if (_smsService == null) userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.60 Safari/537.17";
            Log.Info(userAgent);
            await page.SetUserAgentAsync(userAgent);
        }

        #endregion

        #region FillData

        public async Task TypeSmsCode(Page page)
        {
            var smsCodeInput = await page.QuerySelectorAsync("input#phoneCode");
            if (smsCodeInput != null)
            {
                var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                await page.TypeAsync("input#phoneCode", phoneNumberValidation.Code);
                await _smsService.SetSmsValidationSuccess(_requestId);
                await page.WaitForTimeoutAsync(5000);
                await page.ClickAsync("button[type='submit']");
                var selEula = "div.t-eula-accept button";
                var elEula = await page.QuerySelectorAsync(selEula);
                var eulaButtonVisible = elEula != null && await elEula.IsIntersectingViewportAsync();
                if (eulaButtonVisible) await elEula.ClickAsync();
                await page.WaitForTimeoutAsync(5000);
                _data.Success = true;
            }
        }
        private async Task FillRegistrationData(Page page)
        {
            //await page.GoToAsync(GetRegistrationUrl());

            await FillName(page);
            await FillAccountName(page);


            #region Password

            await page.TypeAsync("input[name=password]", _data.Password);
            await page.TypeAsync("input[name=password_confirm]", _data.Password);

            #endregion

            //#region Phone

            //const string selPhone = "input[name=phone]";
            //await page.ClickAsync(selPhone);
            //await page.EvaluateFunctionAsync("function() {" + $"document.querySelector('{selPhone}').value = ''" + "}");
            //await page.TypeAsync(selPhone, _data.Phone);
            ////await page.ClickAsync("div.registration__send-code button");

            //#endregion

            #region not use yandex wallet

            const string selWallet = "div.form__eula_money span";
            var elWallet = await page.QuerySelectorAsync(selWallet);
            if (elWallet != null) await elWallet.ClickAsync();

            #endregion
        }

        private async Task FillName(Page page)
        {
            var eFirstname = await page.QuerySelectorAsync("input[name=firstname]");
            await eFirstname.TypeAsync(_data.Firstname, _typeOptions);

            var eLastname = await page.QuerySelectorAsync("input[name=lastname]");
            await eLastname.TypeAsync(_data.Lastname, _typeOptions);
        }

        private async Task FillAccountName(Page page)
        {
            if (string.IsNullOrEmpty(_data.AccountName)) _data.AccountName = $"{_data.Firstname.ToLower()}.{_data.Lastname.ToLower()}";

            //await page.TypeAsync("input[name=login]", _data.AccountName);
            const string selAltMail = "li.registration__pseudo-link label";
            //await page.WaitForTimeoutAsync(300);
            //var altMailExists = await page.QuerySelectorAsync(selAltMail);
            if (await EmailAlreadyRegistered(_data.AccountName, page))
            {
                var selAltMailList = $"{selAltMail}";
                var jsAltMailList = $@"Array.from(document.querySelectorAll('{selAltMailList}')).map(a => a.innerText);";
                var altMailList = await page.EvaluateExpressionAsync<string[]>(jsAltMailList);
                var altEmail = altMailList.FirstOrDefault();
                if (string.IsNullOrEmpty(altEmail)) altEmail = altMailList[0];
                _data.AccountName = altEmail.Split('@')[0];
                var idx = Array.IndexOf(altMailList, altEmail);
                var altMailElements = await page.QuerySelectorAllAsync(selAltMailList);
                if (altMailElements != null && altMailElements.Length > idx) await altMailElements[idx].ClickAsync();
            }
        }

        #endregion

    }
}
