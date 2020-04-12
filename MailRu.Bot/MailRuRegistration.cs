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
using System.Threading;
using System.Threading.Tasks;

namespace MailRu.Bot
{
    public class MailRuRegistration : IBot
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MailRuRegistration));

        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;

        public MailRuRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
        }

        private async Task RegistrateByEmail(Page page)
        {
            Log.Info(page.Url);
            await FillName(page);
            await FillBirthdate(page);
            await FillSex(page);
            await FillEmail(page);
            if (page.Url.Contains("light."))
            {
                await page.ClickAsync("button[type='submit']");
            }
            await FillPassword(page);
            if (page.Url.Contains("light."))
            {
                await FillPhone(page);
                await page.WaitForTimeoutAsync(500);
                await page.ClickAsync("button[type='submit']");
            }
            await FillAdditionalEmail(page);
            await page.WaitForTimeoutAsync(1500);
            //await FillPhone(page);
            //await page.WaitForTimeoutAsync(1500);
            var elSignUp = await page.QuerySelectorAsync("div.b-form__control>button");
            if (elSignUp != null)
            {
                await page.ClickAsync("div.b-form__control>button");
            }
            await FillPhone(page);
            await page.WaitForTimeoutAsync(1500);
            elSignUp = await page.QuerySelectorAsync("div.b-form__control>button");
            if (elSignUp != null && await elSignUp.IsIntersectingViewportAsync())
            {
                await page.ClickAsync("div.b-form__control>button");
            }

            await page.WaitForTimeoutAsync(2500);
            var elImgage = await page.QuerySelectorAsync("img.js-captcha-img");
            if (elImgage != null)
            {
                var antiCaptchaOnlineApi = new AntiCaptchaOnlineApi();
                var antiCaptchaResult = antiCaptchaOnlineApi.SolveIm(await elImgage.ScreenshotBase64Async(new ScreenshotOptions { OmitBackground = true }));
                if (antiCaptchaResult.Success)
                {
                    await page.TypeAsync("input[name='capcha']", antiCaptchaResult.Response);
                    await page.ClickAsync("button[data-name='submit']");
                }
                await page.WaitForTimeoutAsync(10000);
                var emailSuccess = await page.QuerySelectorAsync("i#PH_user-email");
                if (emailSuccess != null)
                {
                    _data.Success = true;
                    Log.Info($"emailSuccess: {JsonConvert.SerializeObject(_data)}");
                    await page.ClickAsync("button[data-test-id='onboarding-button-start']");
                }
                else
                {
                    _data.ErrMsg = @"Нет перехода на страницу зарегистрированного email";
                }
            }
        }

        private async Task RegistrateByPhone(Page page)
        {
            Log.Info(page.Url);
            await FillName(page);
            await FillBirthdate(page);
            await FillSex(page);
            await FillEmail(page);
            if (page.Url.Contains("light."))
            {
                await page.ClickAsync("button[type='submit']");
            }
            await FillPassword(page);
            await FillPhone(page);

            var elSignUp = await page.QuerySelectorAsync("div.b-form__control>button");
            if (elSignUp != null)
            {
                await page.ClickAsync("div.b-form__control>button");
            }
            await page.WaitForTimeoutAsync(500);
            var elRecaptcha = await page.QuerySelectorAsync("textarea#g-recaptcha-response");
            if (elRecaptcha != null)
            {
                var antiCaptchaOnlineApi = new AntiCaptchaOnlineApi();
                var antiCaptchaResult = await antiCaptchaOnlineApi.SolveRecaptha("6LckLRMTAAAAAEksaY5akGGK15Yq0n429l5_v-VT", page.Url);
                if (string.IsNullOrEmpty(antiCaptchaResult)) { }

                await page.TypeAsync("textarea#g-recaptcha-response", antiCaptchaResult);
                await page.ClickAsync("button[data-name='submit']");
            }

            //

            // check phone call
            await page.WaitForTimeoutAsync(500);
            var phoneCall = await page.QuerySelectorAsync("#callui-container");
            if (phoneCall == null) await page.WaitForTimeoutAsync(120000);
            if (phoneCall != null)
            {
                Thread.Sleep(1000);
                await page.ClickAsync("#callui-container a"); // I haven't received a call - click link for sms

            }
            // check sms
            await page.WaitForTimeoutAsync(2000);
            var sendSms = await page.QuerySelectorAsync("form input[type='number']");
            // содержимое страницы
            //var content = await page.GetContentAsync();
            //var filename = $"c:\\temp\\mailru_{DateTime.Now.Ticks}.txt";
            //File.WriteAllText(filename, content);
            if (sendSms != null)
            {
                var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                if (phoneNumberValidation != null)
                {
                    // enter sms code
                    await page.TypeAsync("form input[type='number']", phoneNumberValidation.Code);
                    await page.ClickAsync("button[data-name='submit']");
                    await _smsService.SetSmsValidationSuccess(_requestId);
                }
                await page.WaitForTimeoutAsync(10000);
                var emailSuccess = await page.QuerySelectorAsync("i#PH_user-email");
                if (emailSuccess != null)
                {
                    _data.Success = true;
                    Log.Info($"emailSuccess: {JsonConvert.SerializeObject(_data)}");
                    //await _smsService.SetSmsValidationSuccess(_requestId);
                    await page.ClickAsync("button[data-test-id='onboarding-button-start']");

                }
                else
                {
                    _data.ErrMsg = @"Нет перехода на страницу зарегистрированного email";
                }
                await _smsService.SetSmsValidationSuccess(_requestId);
            }
            else
            {
                _data.ErrMsg = "No sms code navigate";
            }
        }

        public async Task<IAccountData> Registration(CountryCode countryCode = CountryCode.RU)
        {
            try
            {
                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                if (_smsService == null)
                {
                    _data.Phone = "79163848169";
                }
                else
                {
                    PhoneNumberRequest phoneNumberRequest = null;
                    //phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, ServiceCode.MailRu);
                    phoneNumberRequest = new PhoneNumberRequest { Id = "444", Phone = "79163848169" };
                    if (phoneNumberRequest == null)
                    {
                        _data.ErrMsg = BotMessages.NoPhoneNumberMessage;
                        return _data;
                    }
                    Log.Info($"phoneNumberRequest: {JsonConvert.SerializeObject(phoneNumberRequest)}");
                    _requestId = phoneNumberRequest.Id;
                    _data.Phone = phoneNumberRequest.Phone.Trim();
                    if (!_data.Phone.StartsWith("+")) _data.Phone = $"+{_data.Phone}";
                    _data.Phone = _data.Phone.Substring(PhoneServiceStore.CountryPrefixes[countryCode].Length + 1);
                }

                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))//var context = await browser.CreateIncognitoBrowserContextAsync();
                using (var page = await browser.NewPageAsync() /*context.NewPageAsync()*/)
                {
                    await SetRequestHook(page);
                    //await SetUserAgent(page);
                    await page.GoToAsync(GetRegistrationUrl());
                    #region comments
                    //await page.EmulateAsync(Puppeteer.Devices[DeviceDescriptorName.IPhone6]);
                    //авторизация прокси
                    //await page.AuthenticateAsync(new Credentials { Username = "hSYPJ1wH", Password = "mR9KJp6F" });

                    //var userPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes("hSYPJ1wH:mR9KJp6F"));
                    //var headers = new Dictionary<string, string>
                    //{
                    //    ["Proxy-Authorization"] = $"Basic {userPassword}"
                    //};
                    //await page.SetExtraHttpHeadersAsync(headers);
                    #endregion comments
                    if (_smsService == null) await RegistrateByEmail(page); else await RegistrateByPhone(page);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _data.ErrMsg = exception.Message;
            }
            return _data;
        }

        private static async void Page_Request(object sender, RequestEventArgs e)
        {
            //if (e.Request.Url.Contains("google"))
            //{
            //    //    //await e.Request.AbortAsync();
            //    Log.Error(e.Request.Url);
            //    if (e.Request.Method == HttpMethod.Post)
            //    {
            //        Log.Error(e.Request.PostData);
            //    }
            //    await e.Request.ContinueAsync();
            //}
            //else
            //{
            //    Log.Info(e.Request.Url);
            //    if (e.Request.Method == HttpMethod.Post)
            //    {
            //        Log.Info(e.Request.PostData);
            //    }
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

        public static async Task<bool> SendEmail(string to, string subject, string[] text, Page page)
        {

            try
            {
                var typeOptions = new TypeOptions { Delay = 50 };
                await page.WaitForTimeoutAsync(2000);
                var selNewLetter = "span.compose-button>span>span";
                if (await page.QuerySelectorAsync(selNewLetter) == null) selNewLetter = "a[data-name=compose] span";
                await page.ClickAsync(selNewLetter);
                await page.WaitForTimeoutAsync(1500);
                var selTo = "div[data-type=to] input";
                if (await page.QuerySelectorAsync(selTo) == null) selTo = "div[data-blockid='compose_to']";
                await page.ClickAsync(selTo);
                await page.TypeAsync(selTo, to, typeOptions);

                var selSubject = "input[name=Subject]";
                //await page.ClickAsync("label[data-for=Subject]") ;
                await page.TypeAsync(selSubject, subject, typeOptions);
                var selText = "div[role=textbox] div div";
                if (await page.QuerySelectorAsync(selText) == null)
                {
                    var elText = await page.QuerySelectorAsync("span.mceEditor iframe");
                    var frame = await elText.ContentFrameAsync();
                    var elBody = await frame.QuerySelectorAsync("body");
                    await elBody.TypeAsync(string.Join(Environment.NewLine, text), typeOptions);
                }
                else
                {
                    await page.ClickAsync(selText);
                    await page.TypeAsync(selText, string.Join(Environment.NewLine, text), typeOptions);
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

        public static async Task<bool> Login(string accountName, string password, Page page)
        {
            //page.EmulateAsync(DeviceDescriptors.Get(DeviceDescriptorName.IPhone6);
            try
            {
                await page.TypeAsync("input[name=Login]", accountName);
                await page.WaitForTimeoutAsync(500);
                await page.ClickAsync("button[type=submit]");
                await page.WaitForTimeoutAsync(500);
                await page.TypeAsync("input[name=Password]", password);
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

        #region FillData
        private async Task FillName(Page page)
        {
            if (page.Url.Contains("light."))
            {
                await page.TypeAsync("div[class*='firstname'] input", _data.Firstname);
                await page.TypeAsync("div[class*='lastname'] input", _data.Lastname);
                return;
            }
            var eFirstname = await page.QuerySelectorAsync("input[name^=firstname]");
            if (eFirstname == null) eFirstname = await page.QuerySelectorAsync("input[name=fname]");
            await eFirstname.TypeAsync(_data.Firstname);

            var eLastname = await page.QuerySelectorAsync("input[name^=lastname]");
            if (eLastname == null) eLastname = await page.QuerySelectorAsync("input[name=lname]");
            await eLastname.TypeAsync(_data.Lastname);
        }

        private async Task FillBirthdate(Page page)
        {
            if (page.Url.Contains("light."))
            {
                await page.ClickAsync("select[class*='day'");
                await page.SelectAsync("select[class*='day'", $"{_data.BirthDate.Day}");
                await page.ClickAsync("select[class*='month'");
                await page.SelectAsync("select[class*='month'", $"{_data.BirthDate.Month}");
                await page.ClickAsync("select[class*='year'");
                await page.SelectAsync("select[class*='year'", $"{_data.BirthDate.Year}");
                return;
            }
            await page.ClickAsync(".b-date__day span");
            await page.ClickAsync($".day{_data.BirthDate.Day} > span");

            await page.ClickAsync(".b-date__month span");
            await page.ClickAsync($".b-date__month a[data-num='{_data.BirthDate.Month - 1}'] > span");

            await page.ClickAsync(".b-date__year span");
            await page.ClickAsync($".b-date__year a[data-value='{_data.BirthDate.Year}'] > span");
        }

        private async Task FillSex(Page page)
        {
            if (page.Url.Contains("light."))
            {
                switch (_data.Sex)
                {
                    case SexCode.Male:
                        await page.ClickAsync("input#man1");
                        break;
                    case SexCode.Female:
                        await page.ClickAsync("input#man2");
                        break;
                }
                return;
            }
            switch (_data.Sex)
            {
                case SexCode.Male:
                    await page.ClickAsync("[value='male']");
                    break;
                case SexCode.Female:
                    await page.ClickAsync("[value='female']");
                    break;
            }
        }

        private async Task FillEmail(Page page)
        {
            if (string.IsNullOrEmpty(_data.AccountName))
            {
                _data.AccountName = $"{_data.Firstname.ToLower()}.{_data.Lastname.ToLower()}";
            }

            //await page.TypeAsync("span.b-email__name>input[type='email']", _data.AccountName);
            const string defaultDomain = "mail.ru";
            if (string.IsNullOrEmpty(_data.Domain))
            {
                _data.Domain = defaultDomain;
            }

            if (page.Url.Contains("light."))
            {
                if (!_data.Domain.ToLower().Equals(defaultDomain))
                {
                    //select domain
                    await page.ClickAsync("select[name='RegistrationDomain'");
                    await page.SelectAsync("select[name='RegistrationDomain'", $"{_data.Domain.ToLower()}");
                }

                await page.ClickAsync("div#loginField input");
                await page.TypeAsync("div#loginField input", _data.AccountName);
                return;
            }

            if (!_data.Domain.ToLower().Equals(defaultDomain))
            {
                //select domain
                await page.ClickAsync("span.b-email__domain span");
                await page.ClickAsync($"a[data-text='@{_data.Domain}']");
            }

            const string selAltMail = "div.b-tooltip_animate";
            //await page.WaitForTimeoutAsync(1000);
            //var altMailExists = await page.QuerySelectorAsync(selAltMail);
            var emailAlreadyRegistered = await EmailAlreadyRegistered(_data.AccountName, _data.Domain, page);
            //if (altMailExists != null)
            if (emailAlreadyRegistered)
            {
                var selAltMailList = $"{selAltMail} div.b-list__item__content";
                var jsAltMailList = $@"Array.from(document.querySelectorAll('{selAltMailList}')).map(a => a.innerText);";
                var altMailList = await page.EvaluateExpressionAsync<string[]>(jsAltMailList);
                var altEmail = altMailList.FirstOrDefault(z => z.Contains(_data.Domain));
                if (string.IsNullOrEmpty(altEmail)) altEmail = altMailList[0];
                _data.AccountName = altEmail.Split('@')[0];
                _data.Domain = altEmail.Split('@')[1];
                var idx = Array.IndexOf(altMailList, altEmail);
                var altMailElements = await page.QuerySelectorAllAsync(selAltMailList);
                if (altMailElements != null && altMailElements.Length > idx) await altMailElements[idx].ClickAsync();
            }
        }

        private async Task FillPassword(Page page)
        {
            if (page.Url.Contains("light."))
            {
                await page.TypeAsync("div[class*=pass] input[type='password']", _data.Password);
                await page.TypeAsync("div[class*=passverify] input[type='password']", _data.Password);
                return;
            }

            await page.TypeAsync("input[name='password']", _data.Password);
            await page.WaitForTimeoutAsync(500);
            await page.TypeAsync("input[name='password_retry']", _data.Password);
            await page.WaitForTimeoutAsync(500);
        }

        private async Task FillAdditionalEmail(Page page)
        {
            //email instead phonenumber
            await page.ClickAsync("div.b-form-field__message>a");
            await page.WaitForSelectorAsync("span.b-email__name input[name='additional_email']");
            await page.WaitForTimeoutAsync(500);
            await page.TypeAsync("span.b-email__name input[name='additional_email']", "pvachovski@bk.ru");
            await page.WaitForTimeoutAsync(1500);

            var elSignUp = await page.QuerySelectorAsync("div.b-form__control>button");
            if (elSignUp != null) await page.ClickAsync("div.b-form__control>button");
        }

        private async Task FillPhone(Page page)
        {
            if (page.Url.Contains("light."))
            {
                await page.TypeAsync("input[name='RemindPhone']", _data.Phone);
                return;
            }

            const string selPhone = "input[type=tel]";
            var elPhone = await page.QuerySelectorAsync(selPhone);
            if (elPhone != null && await elPhone.IsIntersectingViewportAsync())
            {
                await page.ClickAsync(selPhone);
                await page.EvaluateFunctionAsync("function() {" + $"document.querySelector('{selPhone}').value = ''" + "}");
                await page.TypeAsync(selPhone, _data.Phone);
                await page.WaitForTimeoutAsync(1500);
            }
            //else
            //{
            //    Log.Error("Phone input not found");
            //}
        }

        public static string GetRegistrationUrl()
        {
            return @"https://account.mail.ru/signup";
            // @"https://light.mail.ru/signup"
        }

        #endregion FillData

        private async static Task SetRequestHook(Page page)
        {
            //return;
            await page.SetRequestInterceptionAsync(true);
            page.Request += Page_Request;
        }

        private async Task SetUserAgent(Page page)
        {
            var userAgent = UserAgent.GetRandomUserAgent();
            //userAgent = "Opera/9.80 (Windows NT 6.1; U; en-GB) Presto/2.7.62 Version/11.00";
            if (_smsService == null) userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.60 Safari/537.17";
            Log.Info(userAgent);
            await page.SetUserAgentAsync(userAgent);
        }

        public static string GetLoginUrl()
        {
            return @"https://account.mail.ru/login";
        }

        public async static Task<bool> EmailAlreadyRegistered(string accountName, string host, Page page)
        {
            try
            {
                await page.TypeAsync("span.b-email__name>input[type='email']", accountName);
                const string defaultDomain = "mail.ru";
                if (string.IsNullOrEmpty(host))
                {
                    host = defaultDomain;
                }

                if (!host.ToLower().Equals(defaultDomain))
                {
                    //select domain
                    await page.ClickAsync("span.b-email__domain span");
                    await page.ClickAsync($"a[data-text='@{host}']");
                }

                const string selAltMail = "div.b-tooltip_animate";
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
