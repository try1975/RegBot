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
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailRu.Bot
{
    public class MailRuRegistration : IBot
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(MailRuRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;
        private readonly string _mailruProxy = System.Configuration.ConfigurationManager.AppSettings[nameof(_mailruProxy)];
        #endregion

        public MailRuRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            _chromiumSettings.Proxy = _mailruProxy;
        }

        public async Task<IAccountData> Registration(CountryCode countryCode = CountryCode.RU)
        {
            try
            {
                if (!string.IsNullOrEmpty(await SmsServiceInit(countryCode, ServiceCode.MailRu))) return _data;
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
            Log.Info(page.Url);
            await FillName(page);
            await FillBirthdate(page);
            await FillSex(page);
            await FillEmail(page);
            if (page.Url.Contains("light."))
            {
                await ClickSubmit(page);
            }
            await FillPassword(page);
            if (page.Url.Contains("light."))
            {
                await FillPhone(page);
                await page.WaitForTimeoutAsync(500);
                await ClickSubmit(page);
            }
            await FillAdditionalEmail(page);
            await page.WaitForTimeoutAsync(1500);

            await ClickSubmit(page);

            await FillPhone(page);
            await page.WaitForTimeoutAsync(1500);
            await ClickSubmit(page);

            await page.WaitForTimeoutAsync(2500);
            var elImgage = await page.QuerySelectorAsync("img.js-captcha-img");
            if (elImgage != null)
            {
                var antiCaptchaOnlineApi = new AntiCaptchaOnlineApi();
                var antiCaptchaResult = antiCaptchaOnlineApi.SolveIm(await elImgage.ScreenshotBase64Async(new ScreenshotOptions { OmitBackground = true }));
                if (antiCaptchaResult.Success)
                {
                    await page.TypeAsync("input[name='capcha']", antiCaptchaResult.Response);
                    await ClickSubmit(page);
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
                await ClickSubmit(page);
            }
            await FillPassword(page);
            await FillPhone(page);

            await ClickSubmit(page);


            //var elRecaptcha = await page.QuerySelectorAsync("textarea#g-recaptcha-response");
            //if (elRecaptcha != null)
            //{
            //    var antiCaptchaOnlineApi = new AntiCaptchaOnlineApi();
            //    var antiCaptchaResult = await antiCaptchaOnlineApi.SolveRecaptha("6LckLRMTAAAAAEksaY5akGGK15Yq0n429l5_v-VT", page.Url);
            //    if (string.IsNullOrEmpty(antiCaptchaResult)) { }

            //    await page.TypeAsync("textarea#g-recaptcha-response", antiCaptchaResult);
            //    await ClickSubmit(page);
            //}
            await SolveRecaptcha(page);


            //

            // check phone call
            await page.WaitForTimeoutAsync(500);
            var phoneCall = await page.WaitForSelectorAsync("#callui-container", new WaitForSelectorOptions { Timeout = 120 * 1000 });
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
                    await ClickSubmit(page);
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
                await ClickSubmit(page);
                await page.WaitForTimeoutAsync(500);
                await page.TypeAsync("input[name=Password]", password);
                await ClickSubmit(page);
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

        private static async Task ClickSubmit(Page page)
        {
            var elSignUp = await page.QuerySelectorAsync("div.b-form__control>button");
            if (elSignUp == null) elSignUp = await page.QuerySelectorAsync("button[type='submit']");
            if (elSignUp == null) elSignUp = await page.QuerySelectorAsync("button[data-test-id='first-step-submit']");
            if (elSignUp == null) elSignUp = await page.QuerySelectorAsync("button[data-name='submit']");
            if (elSignUp == null) elSignUp = await page.QuerySelectorAsync("button[data-test-id='verification-next-button'] ");

            await elSignUp.ClickAsync();
            await page.WaitForTimeoutAsync(1500);
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
            var eDay = await page.QuerySelectorAsync(".b-date__day span");
            if (eDay == null) eDay = await page.QuerySelectorAsync("div[data-test-id='birth-date__day']");
            await eDay.ClickAsync();
            var eDayClick = await page.QuerySelectorAsync($".day{_data.BirthDate.Day} > span");
            if (eDayClick == null) eDayClick = await page.QuerySelectorAsync($"div[data-test-id='select-value:{_data.BirthDate.Day}']");
            await eDayClick.ClickAsync();

            var eMonth = await page.QuerySelectorAsync(".b-date__month span");
            if (eMonth == null) eMonth = await page.QuerySelectorAsync("div[data-test-id='birth-date__month']");
            await eMonth.ClickAsync();
            var eMonthClick = await page.QuerySelectorAsync($".b-date__month a[data-num='{_data.BirthDate.Month - 1}'] > span");
            if (eMonthClick == null) eMonthClick = await page.QuerySelectorAsync($"div[data-test-id='select-value:{_data.BirthDate.Month}']");
            await eMonthClick.ClickAsync();

            var eYear = await page.QuerySelectorAsync(".b-date__year span");
            if (eYear == null) eYear = await page.QuerySelectorAsync("div[data-test-id='birth-date__year']");
            await eYear.ClickAsync();
            var eYearClick = await page.QuerySelectorAsync($".b-date__year a[data-value='{_data.BirthDate.Year}'] > span");
            if (eYearClick == null) eYearClick = await page.QuerySelectorAsync($"div[data-test-id='select-value:{_data.BirthDate.Year}']");
            await eYearClick.ClickAsync();
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
                var eAltMailList = await page.QuerySelectorAsync(selAltMailList);
                if (eAltMailList == null)
                {
                    selAltMailList = "[data-test-id=account-name-tooltip] a";
                    eAltMailList = await page.QuerySelectorAsync(selAltMailList);
                }
                var altMailElements = await page.QuerySelectorAllAsync(selAltMailList);
                var jsAltMailList = $@"Array.from(document.querySelectorAll('{selAltMailList}')).map(a => a.innerText);";
                var altMailList = await page.EvaluateExpressionAsync<string[]>(jsAltMailList);
                var altEmail = altMailList.FirstOrDefault(z => z.Contains(_data.Domain));
                if (string.IsNullOrEmpty(altEmail)) altEmail = altMailList[0];
                _data.AccountName = altEmail.Split('@')[0];
                _data.Domain = altEmail.Split('@')[1];
                var idx = Array.IndexOf(altMailList, altEmail);
                //altMailElements = await page.QuerySelectorAllAsync(selAltMailList);
                if (altMailElements.Length > idx) await altMailElements[idx].ClickAsync();
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

            var ePassword = await page.QuerySelectorAsync("input[name='password']");
            if (ePassword == null) ePassword = await page.QuerySelectorAsync("input[data-test-id='password-input']");
            await ePassword.TypeAsync(_data.Password);
            await page.WaitForTimeoutAsync(500);
            var ePasswordRetry = await page.QuerySelectorAsync("input[name='password_retry']");
            if (ePasswordRetry == null) ePasswordRetry = await page.QuerySelectorAsync("input[data-test-id='password-confirm-input']");
            await ePasswordRetry.TypeAsync(_data.Password);
            await page.WaitForTimeoutAsync(500);
        }

        private async Task FillAdditionalEmail(Page page)
        {
            //email instead phonenumber
            var eLink = await page.QuerySelectorAsync("div.b-form-field__message>a");
            if (eLink == null) eLink = await page.QuerySelectorAsync("a[data-test-id='phone-number-switch-link']");
            await eLink.ClickAsync();
            await page.WaitForTimeoutAsync(500);
            var eInput = await page.QuerySelectorAsync("span.b-email__name input[name='additional_email']");
            if (eInput == null) eInput = await page.QuerySelectorAsync("input[data-test-id='extra-email']");
            //await page.WaitForTimeoutAsync(500);
            await eInput.TypeAsync("pvachovski@bk.ru");
            await page.WaitForTimeoutAsync(1500);

            await ClickSubmit(page);
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

        private async Task SetUserAgent(Page page)
        {
            var userAgent = UserAgent.GetRandomUserAgent();
            //userAgent = "Opera/9.80 (Windows NT 6.1; U; en-GB) Presto/2.7.62 Version/11.00";
            if (_smsService == null) userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.60 Safari/537.17";
            Log.Info(userAgent);
            await page.SetUserAgentAsync(userAgent);
        }

        private async Task SolveRecaptcha(Page page)
        {
            var eRecaptcha = await page.QuerySelectorAsync("#g-recaptcha-response");
            if (eRecaptcha != null)
            {
                var anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\init.js"));
                anticaptchaScriptText = anticaptchaScriptText.Replace("YOUR-ANTI-CAPTCHA-API-KEY", AntiCaptchaOnlineApi.GetApiKeyAnticaptcha());
                await page.EvaluateExpressionAsync(anticaptchaScriptText);
                await page.AddScriptTagAsync("https://cdn.antcpt.com/imacros_inclusion/recaptcha.js");
                //await page.WaitForSelectorAsync(".antigate_solver.solved", new WaitForSelectorOptions { Timeout = 120 * 1000 });
                //await page.ClickAsync("input[type=submit]");
                //await page.WaitForNavigationAsync(new NavigationOptions { Timeout = 120 * 1000 });
                await SolveRecaptcha(page);
            }
        }

        public static string GetLoginUrl()
        {
            return @"https://account.mail.ru/login";
        }

        public async static Task<bool> EmailAlreadyRegistered(string accountName, string host, Page page)
        {
            var eAccountName = await page.QuerySelectorAsync("span.b-email__name>input[type='email']");
            if (eAccountName == null) eAccountName = await page.QuerySelectorAsync("input[data-test-id='account__input']");
            await eAccountName.TypeAsync(accountName);

            const string defaultDomain = "mail.ru";
            if (string.IsNullOrEmpty(host)) host = defaultDomain;

            if (!host.ToLower().Equals(defaultDomain))
            {
                //select domain
                var eDomain = await page.QuerySelectorAsync("span.b-email__domain span");
                if (eDomain == null) eDomain = await page.QuerySelectorAsync("div[data-test-id='account__select']");
                await eDomain.ClickAsync();
                var eDomainValue = await page.QuerySelectorAsync($"a[data-text='@{host}']");
                if (eDomainValue == null) eDomainValue = await page.QuerySelectorAsync($"div[data-test-id='select-value:@{host}']");
                await eDomainValue.ClickAsync();
            }

            await page.WaitForTimeoutAsync(1000);
            var altMailExists = await page.QuerySelectorAsync("div.b-tooltip_animate");
            if (altMailExists == null) altMailExists = await page.QuerySelectorAsync("[data-test-id='exists']");
            return altMailExists != null;
        }
    }
}
