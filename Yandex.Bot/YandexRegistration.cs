using AnticaptchaOnline;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex.Bot
{
    public partial class YandexRegistration : RegistrationBot.Bot
    {
        #region fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(YandexRegistration));
        public const string RegistrationUrl = @"https://passport.yandex.ru/registration/mail?from=mail&origin=home_desktop_ru&retpath=https%3A%2F%2Fmail.yandex.ru%2F";
        //https://passport.yandex.ru/auth/reg?from=mail&origin=home_desktop_ru&retpath=https%3A%2F%2Fmail.yandex.ru%2F
        #endregion

        public YandexRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings) : base(data, smsService, chromiumSettings) { }

        #region override
        protected override ServiceCode GetServiceCode() => ServiceCode.Yandex;

        protected override async Task StartRegistration(Page page)
        {
            await page.GoToAsync(GetRegistrationUrl());
            if (_smsService == null) await RegistrateByEmail(page); else await RegistrateByPhone(page);
        }
        #endregion
        
        private string GetRegistrationUrl() => RegistrationUrl;

        private async Task RegistrateByEmail(Page page)
        {
            //await page.GoToAsync(GetRegistrationUrl());
            await FillName(page);
            await FillAccountName(page);
            await FillPassword(page);
            await NotUseYandexWallet(page);

            await page.ClickAsync("span.link_has-no-phone");
            await page.TypeAsync("input#hint_answer", "ахинея", _typeOptions);
            //todo: cycle 3 time solve
            await SolveImageCaptcha(page);
            await ClickSubmit(page);
            await page.WaitForTimeoutAsync(1000);

            if (await IsImageCaptchaSolved(page))
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
            await FillPhone1(page); // maybe enter phone
            await FillName(page);
            await FillAccountName(page);
            await FillPassword(page);
            await FillPhone(page);
            //todo: check phone valid
            await NotUseYandexWallet(page);
            await ClickSubmit(page);

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

        private async Task ClickSubmit(Page page)
        {
            var elSignUp = await page.QuerySelectorAsync("button[type=submit]");
            await elSignUp.ClickAsync();
            await page.WaitForTimeoutAsync(500);
        }

        #region Steps

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

        private async Task FillPhone1(Page page)
        {
            var ePhone = await page.QuerySelectorAsync("input[nampe=phone]");
            if (ePhone == null) return;
            await ePhone.ClickAsync();
            await ePhone.TypeAsync(_data.Phone, _typeOptions);
            await ClickSubmit(page);
        }

        private async Task FillPhone(Page page)
        {
            const string selPhone = "input[name=phone]";
            await page.ClickAsync(selPhone);
            await page.EvaluateFunctionAsync("function() {" + $"document.querySelector('{selPhone}').value = ''" + "}");
            await page.TypeAsync(selPhone, _data.Phone, _typeOptions);
            ////await page.ClickAsync("div.registration__send-code button");
        }

        private async Task FillPassword(Page page)
        {
            var ePassword = await page.QuerySelectorAsync("input[name=password]");
            await ePassword.TypeAsync(_data.Password, _typeOptions);
            var ePasswordConfirm = await page.QuerySelectorAsync("input[name=password_confirm]");
            await ePasswordConfirm.TypeAsync(_data.Password, _typeOptions);
        }

        private async Task NotUseYandexWallet(Page page)
        {
            var elWallet = await page.QuerySelectorAsync("div.form__eula_money span");
            if (elWallet != null) await elWallet.ClickAsync();
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

        private async Task SolveImageCaptcha(Page page)
        {
            var svgImage = await page.QuerySelectorAsync("img.captcha__image");
            if (svgImage != null)
            {
                var antiCaptchaOnlineApi = new AntiCaptchaOnlineApi();
                var antiCaptchaResult = antiCaptchaOnlineApi.SolveIm(await svgImage.ScreenshotBase64Async(new ScreenshotOptions { OmitBackground = true }));
                if (antiCaptchaResult.Success) await page.TypeAsync("input#captcha", antiCaptchaResult.Response);
            }
        }

        private async Task<bool> IsImageCaptchaSolved(Page page)
        {
            var eCaptchaError = await page.QuerySelectorAsync("div[data-t='captcha-error']");
            return eCaptchaError == null;
        }

        #endregion
    }
}
