using AnticaptchaOnline;
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
    public partial class YandexRegistration : IBot
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(YandexRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;
        //private readonly string _yandexProxy = System.Configuration.ConfigurationManager.AppSettings[nameof(_yandexProxy)];
        private static readonly TypeOptions _typeOptions = new TypeOptions { Delay = 50 };
        #endregion

        public YandexRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = "yandex.ru";
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            //_chromiumSettings.Proxy = _yandexProxy;
            //_chromiumSettings.Proxy = _chromiumSettings.GetProxy(ServiceCode.Yandex);
        }

        #region Registrate
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

        private async Task RegistrateByEmail(Page page)
        {
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
            // maube enter phone
            await FillName(page);
            await FillAccountName(page);

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
        #endregion

        
        
    }
}
