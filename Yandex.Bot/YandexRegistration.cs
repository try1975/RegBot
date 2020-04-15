﻿using AnticaptchaOnline;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex.Bot
{
    public class YandexRegistration : IBot
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(YandexRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;
        private readonly string _yandexProxy = System.Configuration.ConfigurationManager.AppSettings[nameof(_yandexProxy)];
        #endregion

        public YandexRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = "yandex.ru";
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            _chromiumSettings.Proxy = _yandexProxy;
        }

        public async Task<IAccountData> Registration(CountryCode countryCode = CountryCode.RU)
        {
            try
            {
                if (!string.IsNullOrEmpty(await SmsServiceInit(countryCode, ServiceCode.Yandex))) return _data;
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
                using (var page = await PageInit(browser)) if (_smsService == null) await RegistrateByEmail(page); else await RegistrateByPhone(page);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _data.ErrMsg = exception.Message;
            }
            return _data;
        }

        private async Task RegistrateByEmail(Page page) {
            await FillRegistrationData(page);

            await page.ClickAsync("span.link_has-no-phone");
            await page.TypeAsync("input#hint_answer", "ахинея");

            var svgImage = await page.QuerySelectorAsync("img.captcha__image");
            if (svgImage != null)
            {
                var antiCaptchaOnlineApi = new AntiCaptchaOnlineApi();
                var antiCaptchaResult = antiCaptchaOnlineApi.SolveIm(await svgImage.ScreenshotBase64Async(new ScreenshotOptions { OmitBackground = true }));
                if (antiCaptchaResult.Success) await page.TypeAsync("input#captcha", antiCaptchaResult.Response);
            }

            await page.ClickAsync("button[type='submit']");
            await page.WaitForTimeoutAsync(1000);
            if (await page.QuerySelectorAsync("div[data-t='captcha-error']") == null)
            {
                var selEula = "div.t-eula-accept button";
                var elEula = await page.QuerySelectorAsync(selEula);
                var eulaButtonVisible = elEula != null && await elEula.IsIntersectingViewportAsync();
                if (eulaButtonVisible) await elEula.ClickAsync();
                await page.WaitForTimeoutAsync(5000);
                _data.Success = true;
            }
        }

        private async Task RegistrateByPhone(Page page)
        {
            await page.ClickAsync("div.registration__send-code span");
            //await page.WaitForTimeoutAsync(2000);
            //const string smsSendSelector = "div.reg-field__popup span.registration__pseudo-link";
            //ElementHandle smsCodeInput;
            //var phoneCodeLabelTextToken = await page.EvaluateExpressionAsync("document.querySelector('label[for=phoneCode').innerText");
            //var isSms = false;
            //var isVoice = false;
            //if (phoneCodeLabelTextToken != null)
            //{
            //    var phoneCodeLabelText = phoneCodeLabelTextToken.ToString();
            //    if (phoneCodeLabelText.Contains("смс")) isSms = true;
            //    if (phoneCodeLabelText.Contains("голос")) isVoice = true;
            //}
            //// ошибка превышен лимит sms
            ////var err = await page.QuerySelectorAsync("div.error-message");
            ////if (smsSendExists == null && smsCodeInput == null && err ==null) await page.WaitForTimeoutAsync(20000);

            //if (isVoice)
            //{
            //    await page.WaitForTimeoutAsync(35000);
            //    var jsAltAction = $@"Array.from(document.querySelectorAll('{smsSendSelector}')).map(a => a.innerText);";
            //    var linkList = await page.EvaluateExpressionAsync<string[]>(jsAltAction);
            //    var smsLink = linkList.FirstOrDefault(z => z.Contains("sms"));
            //    if (!string.IsNullOrEmpty(smsLink))
            //    {
            //        var idx = Array.IndexOf(linkList, smsLink);
            //        var altMailElements = await page.QuerySelectorAllAsync(smsSendSelector);
            //        if (altMailElements != null && altMailElements.Length > idx)
            //        {
            //            await altMailElements[idx].ClickAsync();
            //            var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
            //            Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
            //            if (phoneNumberValidation != null)
            //            {
            //                await _smsService.SetSmsValidationSuccess(_requestId);
            //                smsCodeInput = await page.QuerySelectorAsync("input#phoneCode");
            //                if (smsCodeInput != null)
            //                {
            //                    await page.TypeAsync("input#phoneCode", phoneNumberValidation.Code);
            //                    await page.WaitForTimeoutAsync(5000);
            //                    await page.ClickAsync("button[type='submit']");
            //                    await page.WaitForTimeoutAsync(5000);
            //                    _data.Success = true;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        await TypeSmsCode(page);
            //        #region comments
            //        //smsCodeInput = await page.QuerySelectorAsync("input#phoneCode");
            //        //if (smsCodeInput != null)
            //        //{
            //        //    var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
            //        //    Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
            //        //    await page.TypeAsync("input#phoneCode", phoneNumberValidation.Code);
            //        //    await _smsService.SetSmsValidationSuccess(_requestId);
            //        //    await page.WaitForTimeoutAsync(5000);
            //        //    await page.ClickAsync("button[type='submit']");
            //        //    await page.WaitForTimeoutAsync(5000);
            //        //    _data.Success = true;
            //        //}
            //        #endregion
            //    }
            //}

            //if (isSms)
            //{
            //    await TypeSmsCode(page);
            //    #region comments
            //    //smsCodeInput = await page.QuerySelectorAsync("input#phoneCode");
            //    //if (smsCodeInput != null)
            //    //{
            //    //    var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
            //    //    Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
            //    //    await page.TypeAsync("input#phoneCode", phoneNumberValidation.Code);
            //    //    await _smsService.SetSmsValidationSuccess(_requestId);
            //    //    await page.WaitForTimeoutAsync(5000);
            //    //    await page.ClickAsync("button[type='submit']");
            //    //    var selEula = "div.t-eula-accept button";
            //    //    var elEula = await page.QuerySelectorAsync(selEula);
            //    //    var eulaButtonVisible = elEula != null && await elEula.IsIntersectingViewportAsync();
            //    //    if (eulaButtonVisible) await elEula.ClickAsync();
            //    //    await page.WaitForTimeoutAsync(5000);
            //    //    _data.Success = true;
            //    //}
            //    #endregion
            //}
        }

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

        private async Task<Page> PageInit(Browser browser)
        {
            var context = await browser.CreateIncognitoBrowserContextAsync();
            var page = await context.NewPageAsync();
            //var page = await browser.NewPageAsync();
            #region commented
            //await SetRequestHook(page);
            await SetUserAgent(page);
            //await page.EmulateAsync(Puppeteer.Devices[DeviceDescriptorName.IPhone6]); 
            #endregion
            await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
            await page.GoToAsync(GetRegistrationUrl());
            return page;
        }

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
                var typeOptions = new TypeOptions { Delay = 50 };
                await page.GoToAsync("https://mail.yandex.ru/");
                var selNewLetter = "span.mail-ComposeButton-Text";
                await page.WaitForSelectorAsync(selNewLetter);
                await page.ClickAsync(selNewLetter);
                await page.WaitForTimeoutAsync(1500);
                var selTo = "div[name=to]";
                await page.ClickAsync(selTo);
                await page.TypeAsync(selTo, to, typeOptions);
                var selSubject = "input[name ^= subj]";
                await page.ClickAsync(selSubject);
                await page.TypeAsync(selSubject, subject, typeOptions);
                var selText = "div[role=textbox]";
                await page.ClickAsync(selText);
                await page.TypeAsync(selText, string.Join(Environment.NewLine, text), typeOptions);
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

        private async Task FillRegistrationData(Page page)
        {
            await page.GoToAsync(GetRegistrationUrl());

            #region Name

            await page.TypeAsync("input[name=firstname]", _data.Firstname);
            await page.TypeAsync("input[name=lastname]", _data.Lastname);

            #endregion

            #region Login

            if (string.IsNullOrEmpty(_data.AccountName))
            {
                _data.AccountName = $"{_data.Firstname.ToLower()}.{_data.Lastname.ToLower()}";
            }

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

            #endregion

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

        public static string GetRegistrationUrl()
        {
            return @"https://passport.yandex.ru/registration/mail?from=mail&origin=home_desktop_ru&retpath=https%3A%2F%2Fmail.yandex.ru%2F";
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

        public static string GetLoginUrl()
        {
            return @"https://passport.yandex.ru/auth";
        }

        public static async Task<bool> EmailAlreadyRegistered(string accountName, Page page)
        {
            try
            {
                await page.TypeAsync("input[name=login]", accountName);
                const string selAltMail = "div[data-t='login-error']";
                await page.WaitForTimeoutAsync(1000);
                var altMailExists = await page.QuerySelectorAsync(selAltMail);
                if (altMailExists == null) return false;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            return true;
        }
    }
}
