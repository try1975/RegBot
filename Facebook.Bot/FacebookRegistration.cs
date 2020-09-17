using AnticaptchaOnline;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using PuppeteerSharp.Mobile;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Bot
{
    public class FacebookRegistration : RegistrationBot.Bot
    {
        #region init
        #region fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(FacebookRegistration));
        public const string RegistrationUrl = @"https://www.facebook.com/r.php"; //https://m.facebook.com/ https://www.facebook.com/
        #endregion

        public FacebookRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings) : base(data, smsService, chromiumSettings)
        {
            var proxy = _chromiumSettings.GetProxy(ServiceCode.Facebook);
            if (!string.IsNullOrEmpty(proxy)) _chromiumSettings.Proxy = proxy;
        }

        #region infra
        protected override ServiceCode GetServiceCode() => ServiceCode.Facebook;

        protected override async Task StartRegistration(Page page)
        {
            await page.GoToAsync(GetRegistrationUrl());
            await RegistrateByPhone(page);
        }

        protected override string GetRegistrationUrl() => RegistrationUrl;

        protected override DeviceDescriptorName GetDeviceDescriptorName()
        {
            return DeviceDescriptorName.IPhoneXLandscape;
        }
        #endregion 
        #endregion

        //public async Task<IAccountData> Registration(CountryCode countryCode)
        //{
        //    if (!string.IsNullOrEmpty(await SmsServiceInit(countryCode, ServiceCode.Facebook))) return _data;
        //    using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
        //    {
        //        try
        //        {
        //            using (var page = await PageInit(browser)) await RegistrateByPhone(page);
        //            await _smsService.SetSmsValidationSuccess(_requestId);
        //        }
        //        catch (Exception exception)
        //        {
        //            Log.Error(exception);
        //            if (string.IsNullOrEmpty(_data.ErrMsg)) _data.ErrMsg = exception.Message;
        //            await _smsService.SetNumberFail(_requestId);
        //        }
        //        finally
        //        {
        //            await browser.CloseAsync();
        //        }
        //    }
        //    return _data;
        //}

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

            var eVerify = await page.QuerySelectorAsync("#checkpointSubmitButton");
            if (eVerify != null) { 
                await eVerify.ClickAsync();
                try { await page.WaitForNavigationAsync(_navigationOptions); } catch { }
                //var pages = await page.Browser.PagesAsync();
                //page = pages[0];
            }
            //await SolveRecaptcha(page);
            await FillPhoneAgain(page);

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

        #region Steps
        private async Task FillRegistrationData(Page page)
        {
            var _typeOptions = new TypeOptions { Delay = 50 };
            var eSignUp = await page.QuerySelectorAsync("#signup-button");
            if (eSignUp != null) await eSignUp.ClickAsync();


            #region Name
            await page.WaitForSelectorAsync("input[name=firstname]", new WaitForSelectorOptions { Visible = true });
            await page.ClickAsync("input[name=firstname]");
            await page.TypeAsync("input[name=firstname]", _data.Firstname, _typeOptions);
            await page.TypeAsync("input[name=lastname]", _data.Lastname, _typeOptions);


            #endregion

            await page.TypeAsync("input[name=reg_email__]", _data.Phone, _typeOptions);
            await page.TypeAsync("input[name=reg_passwd__]", _data.Password, _typeOptions);


            await page.WaitForTimeoutAsync(500);
            await page.ClickAsync("select#day");
            await page.WaitForTimeoutAsync(500);
            await page.SelectAsync("select#day", $"{_data.BirthDate.Day}");
            await page.ClickAsync("select#month");
            await page.WaitForTimeoutAsync(500);
            await page.SelectAsync("select#month", $"{_data.BirthDate.Month}");
            await page.WaitForTimeoutAsync(500);
            await page.ClickAsync("select#year");
            await page.SelectAsync("select#year", $"{_data.BirthDate.Year}");


            if (_data.Sex == SexCode.Female) await page.ClickAsync("input[name=sex][value='1']");
            if (_data.Sex == SexCode.Male) await page.ClickAsync("input[name=sex][value='2']");
        }

        private async Task SolveRecaptcha(Page page, int deep = 0)
        {
            if (deep >= 2) return;
            var eGotoRecapthcha = await page.QuerySelectorAsync("#checkpointSubmitButton");
            if (eGotoRecapthcha == null) return;
            await eGotoRecapthcha.ClickAsync();
            await page.WaitForTimeoutAsync(2000);
            var eRecaptcha = await page.QuerySelectorAsync("#captcha_response");
            if (eRecaptcha != null)
            {
                //var targets = page.Browser.Targets();
                var anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\init.js"));
                anticaptchaScriptText = anticaptchaScriptText.Replace("YOUR-ANTI-CAPTCHA-API-KEY", AntiCaptchaOnlineApi.GetApiKeyAnticaptcha());
                await page.EvaluateExpressionAsync(anticaptchaScriptText);
                anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\recaptchaaifb.js"));
                await page.EvaluateExpressionAsync(anticaptchaScriptText);
                //targets[8].

                //await page.AddScriptTagAsync(new AddTagOptions { Content= anticaptchaScriptText });
                //await page.WaitForSelectorAsync(".antigate_solver.solved", new WaitForSelectorOptions { Timeout = 120 * 1000 });
                // await page.ClickAsync("input[type=submit]");
                //await page.WaitForNavigationAsync();
                try { await page.WaitForTimeoutAsync(90 * 1000); } catch { }
                await SolveRecaptcha(page, ++deep);
            }
        }

        private async Task ClickSubmit(Page page)
        {
            var elSignUp = await page.QuerySelectorAsync("button[type=submit]");
            await elSignUp.ClickAsync();
            await page.WaitForTimeoutAsync(500);
        }

        private async Task FillPhoneAgain(Page page)
        {
            var eSendCode = await page.QuerySelectorAsync("button[name='submit[set_contact_point_primary_button]']");
            if (eSendCode == null) return;
            var selCountries = "ul[role=menu] a>span>span";
            var eCountries = await page.QuerySelectorAllAsync(selCountries);
            if (eCountries == null) return;
            var jsAltMailList = $@"Array.from(document.querySelectorAll('{selCountries}')).map(a => a.innerText);";
            var countries = await page.EvaluateExpressionAsync<string[]>(jsAltMailList);
            // код страны +44
            var country = countries.FirstOrDefault(z => z.Contains($"(+{_countryPrefix})"));
            var idx = Array.IndexOf(countries, country);
            if (eCountries.Length > idx) await eCountries[idx].ClickAsync();

            var ePhone = await page.QuerySelectorAsync("input[type=tel]");
            await ePhone.TypeAsync(_data.Phone.Replace($"+{_countryPrefix}", ""), _typeOptions);

            await eSendCode.ClickAsync();
        }
        #endregion

        #region else
        private async static Task SetHooks(Page page)
        {
            //await page.SetRequestInterceptionAsync(true);

            //page.Request += Page_Request;
            //page.Response += Page_Response;

            page.FrameAttached += Page_FrameAttached;
            page.FrameNavigated += Page_FrameNavigated;

            page.WorkerCreated += Page_WorkerCreated;
        }

        private static void Page_WorkerCreated(object sender, WorkerEventArgs e)
        {
            Log.Info($"{nameof(Page_WorkerCreated)} {e.Worker.Url}");
        }

        private async static void Page_FrameNavigated(object sender, FrameEventArgs e)
        {
            Log.Info($"{nameof(Page_FrameNavigated)} {e.Frame.Url}");
            //if (e.Frame.Url.Contains("referer_frame"))
            //{
            //    var anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\recaptchaaifb.js"));
            //    //await e.Frame.AddScriptTagAsync(new AddTagOptions { Url = "https://cdn.antcpt.com/imacros_inclusion/recaptcha.js" });
            //    await e.Frame.AddScriptTagAsync(new AddTagOptions { Content = anticaptchaScriptText });
            //}
        }

        private async static void Page_FrameAttached(object sender, FrameEventArgs e)
        {
            Log.Info($"{nameof(Page_FrameAttached)} {e.Frame.Url}");
            //var anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\init.js"));
            //anticaptchaScriptText = anticaptchaScriptText.Replace("YOUR-ANTI-CAPTCHA-API-KEY", AntiCaptchaOnlineApi.GetApiKeyAnticaptcha());
            //await e.Frame.EvaluateExpressionAsync(anticaptchaScriptText);

            //anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\recaptchaaifb.js"));
            //await e.Frame.EvaluateExpressionAsync(anticaptchaScriptText);
            //await e.Frame.AddScriptTagAsync(new AddTagOptions { Url = "https://cdn.antcpt.com/imacros_inclusion/recaptcha.js" });
        }

        private static async void Page_Response(object sender, ResponseCreatedEventArgs e)
        {
            Log.Info($"Page_Response {e.Response.Request.Url}");
            if (e.Response.Request.Url.Contains("referer_frame"))
            {
                var body = await e.Response.TextAsync();
                //await e.Response.Request.RespondAsync(new ResponseData { Body = body });
            }

        }

        private static async void Page_Request(object sender, RequestEventArgs e)
        {
            //if (e.Request.Url.Contains("referer_frame"))
            //{
            //    //    //await e.Request.AbortAsync();
            //    Log.Info(e.Request.Url);
            //    await e.Request.ContinueAsync();
            //    var body = await e.Request.Response.TextAsync();
            //    await e.Request.RespondAsync(new ResponseData { Body = body });
            //}
            //else
            //{
            //    Log.Info(e.Request.Url);
            //    await e.Request.ContinueAsync();
            //}

            await e.Request.ContinueAsync();
            //var payload = new Payload()
            //{
            //    Url = "https://httpbin.org/forms/post",
            //    Method = HttpMethod.Post /*,
            //    PostData = keyValuePairs*/
            //};
            //await e.Request.ContinueAsync(payload);
        } 
        #endregion
    }
}
