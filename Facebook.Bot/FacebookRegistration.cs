using AnticaptchaOnline;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Facebook.Bot
{
    public class FacebookRegistration : IBot
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FacebookRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;
        private readonly string _fbProxy = System.Configuration.ConfigurationManager.AppSettings[nameof(_fbProxy)];

        public FacebookRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = "facebook.com";
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            _chromiumSettings.Proxy = _fbProxy;
        }

        public async Task<IAccountData> Registration(CountryCode countryCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(await SmsServiceInit(countryCode, ServiceCode.Facebook))) return _data;
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

        public async Task RegistrateByPhone(Page page)
        {

            await FillRegistrationData(page);
            await page.WaitForTimeoutAsync(3000);
            await ClickSubmit(page);

            // check phone accepted
            try
            {
                await page.WaitForNavigationAsync();
            }
            catch (Exception exception)
            {
                Log.Debug(exception);
                await _smsService.SetNumberFail(_requestId);
                _data.ErrMsg = BotMessages.PhoneNumberNotAcceptMessage;
                return;
            }
            await page.WaitForTimeoutAsync(3000);
            await SolveRecaptcha(page);


            var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
            Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
            if (phoneNumberValidation != null)
            {
                await _smsService.SetSmsValidationSuccess(_requestId);
                const string selSmsCode = "input#code_in_cliff";
                await page.ClickAsync(selSmsCode);
                await page.TypeAsync(selSmsCode, phoneNumberValidation.Code);
                await page.ClickAsync("div.uiInterstitialContent button");
                await page.WaitForSelectorAsync("div._t a", new WaitForSelectorOptions { Timeout = 20000 });
                await page.ClickAsync("div._t a");

                await page.WaitForNavigationAsync();
                await page.WaitForTimeoutAsync(3000);
                _data.Success = true;
            }
            else await _smsService.SetNumberFail(_requestId);

        }

        private async Task FillRegistrationData(Page page)
        {
            var eSignUp = await page.QuerySelectorAsync("#signup-button");
            if(eSignUp!=null) await eSignUp.ClickAsync();


            #region Name
            await page.WaitForSelectorAsync("input[name=firstname]", new WaitForSelectorOptions { Visible = true });
            await page.ClickAsync("input[name=firstname]");
            await page.TypeAsync("input[name=firstname]", _data.Firstname);
            await page.TypeAsync("input[name=lastname]", _data.Lastname);


            #endregion

            await page.TypeAsync("input[name=reg_email__]", _data.Phone);
            await page.TypeAsync("input[name=reg_passwd__]", _data.Password);


            await page.ClickAsync("select#day");
            await page.SelectAsync("select#day", $"{_data.BirthDate.Day}");
            await page.ClickAsync("select#month");
            await page.SelectAsync("select#month", $"{_data.BirthDate.Month}");
            await page.ClickAsync("select#year");
            await page.SelectAsync("select#year", $"{_data.BirthDate.Year}");


            if (_data.Sex == SexCode.Female) await page.ClickAsync("input[name=sex][value='1']");
            if (_data.Sex == SexCode.Male) await page.ClickAsync("input[name=sex][value='2']");
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

        private async Task<Page> PageInit(Browser browser, bool isIncognito = true)
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
            //await SetUserAgent(page);
            //await page.EmulateAsync(Puppeteer.Devices[DeviceDescriptorName.IPhone6]); 
            #endregion
            await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
            await page.GoToAsync(GetRegistrationUrl());
            return page;
        }

        private async Task SolveRecaptcha(Page page)
        {
            var eGotoRecapthcha = await page.QuerySelectorAsync("#checkpointSubmitButton");
            if (eGotoRecapthcha == null) return;
            await eGotoRecapthcha.ClickAsync();
            await page.WaitForTimeoutAsync(2000);
            var eRecaptcha = await page.QuerySelectorAsync("#captcha_response");
            if (eRecaptcha != null)
            {
                var anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\init.js"));
                anticaptchaScriptText = anticaptchaScriptText.Replace("YOUR-ANTI-CAPTCHA-API-KEY", AntiCaptchaOnlineApi.GetApiKeyAnticaptcha());
                await page.EvaluateExpressionAsync(anticaptchaScriptText);
                anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\recaptchaai.js"));
                await page.EvaluateExpressionAsync(anticaptchaScriptText);
                //await page.AddScriptTagAsync(new AddTagOptions { Content= anticaptchaScriptText });
                //await page.WaitForSelectorAsync(".antigate_solver.solved", new WaitForSelectorOptions { Timeout = 120 * 1000 });
                await page.ClickAsync("input[type=submit]");
                await page.WaitForNavigationAsync();
                await SolveRecaptcha(page);
            }
        }

        private async Task ClickSubmit(Page page)
        {
            var elSignUp = await page.QuerySelectorAsync("button[type=submit]");
            await elSignUp.ClickAsync();
            await page.WaitForTimeoutAsync(500);
        }

        public static string GetRegistrationUrl()
        {
            return @"https://www.facebook.com/";
            //https://m.facebook.com/
        }

        private async Task SetUserAgent(Page page)
        {
            var userAgent = UserAgent.GetRandomUserAgent();
            //userAgent = "Opera/9.80 (Windows NT 6.1; U; en-GB) Presto/2.7.62 Version/11.00";
            if (_smsService == null) userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.60 Safari/537.17";
            Log.Info(userAgent);
            await page.SetUserAgentAsync(userAgent);
        }
    }
}
