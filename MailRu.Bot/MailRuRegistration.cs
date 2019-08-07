using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerSharp;

namespace MailRu.Bot
{
    public class MailRuRegistration : IBot
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MailRuRegistration));

        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly string _chromiumPath;

        public MailRuRegistration(IAccountData data, ISmsService smsService, string chromiumPath)
        {
            _data = data;
            _smsService = smsService;
            if (string.IsNullOrEmpty(chromiumPath)) chromiumPath = Environment.CurrentDirectory;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            _chromiumPath = chromiumPath;
            Log.Debug($"_chromiumPath: {_chromiumPath}");
        }

        public async Task<IAccountData> Registration(CountryCode countryCode = CountryCode.RU, bool headless = true)
        {
            try
            {
                var options = new LaunchOptions
                {
                    Headless = headless,
                    ExecutablePath = _chromiumPath,
                    //SlowMo = 10,

                };

                //options.Args = new[]
                //{
                //    "--proxy-server=socks4://36.67.184.157:54555"//, "--proxy-auth: userx:passx", "--proxy-type: 'meh'"
                //};
                //https://blog.apify.com/how-to-make-headless-chrome-and-puppeteer-use-a-proxy-server-with-authentication-249a21a79212
                //https://toster.ru/q/562104

                // windows7 websocket https://github.com/PingmanTools/System.Net.WebSockets.Client.Managed
                if (Environment.OSVersion.VersionString.Contains("NT 6.1")) { options.WebSocketFactory = WebSocketFactory; }
                //using (var browser = await Puppeteer.LaunchAsync(options))
                //using (var page = await browser.NewPageAsync())
                //{
                //    await page.GoToAsync("https://yandex.ru/internet/");
                //}

                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, MailServiceCode.MailRu);
                //var phoneNumberRequest = new PhoneNumberRequest{Id = "444", Phone = "79163848169"};
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

                using (var browser = await Puppeteer.LaunchAsync(options))
                using (var page = await browser.NewPageAsync())
                {
                    await FillRegistrationData(page);
                    await page.ClickAsync("div.b-form__control>button");

                    // TODO check captcha
                    // check phone call
                    await page.WaitForTimeoutAsync(500);
                    // ReSharper disable once StringLiteralTypo
                    var phoneCall = await page.QuerySelectorAsync("#callui-container");
                    //if (phoneCall == null) await page.WaitForTimeoutAsync(120000);
                    if (phoneCall != null)
                    {
                        Thread.Sleep(1000);
                        // ReSharper disable once StringLiteralTypo
                        await page.ClickAsync("#callui-container a"); // I haven't received a call - click link for sms

                    }
                    // check sms
                    await page.WaitForTimeoutAsync(2000);
                    var sendSms = await page.QuerySelectorAsync("form input[type='number']");
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
                            await page.WaitForTimeoutAsync(10000);
                            var emailSuccess = await page.QuerySelectorAsync("i#PH_user-email");
                            if (emailSuccess != null)
                            {
                                _data.Success = true;
                                Log.Info($"emailSuccess: {JsonConvert.SerializeObject(_data)}");
                                await _smsService.SetSmsValidationSuccess(_requestId);
                                // ReSharper disable once StringLiteralTypo
                                await page.ClickAsync("button[data-test-id='onboarding-button-start']");

                            }
                            else
                            {
                                _data.ErrMsg = @"Нет перехода на страницу зарегистрированного email";
                            }
                            //await _smsService.SetSmsValidationSuccess(_requestId);
                        }
                    }
                    else
                    {
                        _data.ErrMsg = "No sms code navigate";
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _data.ErrMsg = exception.Message;
            }
            return _data;
        }

        private async Task<WebSocket> WebSocketFactory(Uri url, IConnectionOptions options,
            CancellationToken cancellationToken)
        {
            var ws = new System.Net.WebSockets.Managed.ClientWebSocket();
            await ws.ConnectAsync(url, cancellationToken);
            return ws;
        }

        private async Task FillRegistrationData(Page page)
        {
            await page.GoToAsync("https://account.mail.ru/signup");

            #region Name

            await page.TypeAsync("input[name^=firstname]", _data.Firstname);
            await page.TypeAsync("input[name^=lastname]", _data.Lastname);

            #endregion

            #region Birthdate

            await page.ClickAsync(".b-date__day span");
            await page.ClickAsync($".day{_data.BirthDate.Day} > span");

            await page.ClickAsync(".b-date__month span");
            await page.ClickAsync($".b-date__month a[data-num='{_data.BirthDate.Month - 1}'] > span");

            await page.ClickAsync(".b-date__year span");
            await page.ClickAsync($".b-date__year a[data-value='{_data.BirthDate.Year}'] > span");

            #endregion

            switch (_data.Sex)
            {
                case SexCode.Male:
                    await page.ClickAsync("[value='male']");
                    break;
                case SexCode.Female:
                    await page.ClickAsync("[value='female']");
                    break;
            }

            #region Email

            if (string.IsNullOrEmpty(_data.AccountName))
            {
                _data.AccountName = $"{_data.Firstname.ToLower()}.{_data.Lastname.ToLower()}";
            }

            await page.TypeAsync("span.b-email__name>input[type='email']", _data.AccountName);
            const string defaultDomain = "mail.ru";
            if (string.IsNullOrEmpty(_data.Domain))
            {
                _data.Domain = defaultDomain;
            }

            if (!_data.Domain.ToLower().Equals(defaultDomain))
            {
                //select domain
                await page.ClickAsync("span.b-email__domain span");
                await page.ClickAsync($"a[data-text='@{_data.Domain}']");
            }

            const string selAltMail = "div.b-tooltip_animate";
            await page.WaitForTimeoutAsync(1000);
            var altMailExists = await page.QuerySelectorAsync(selAltMail);
            if (altMailExists != null)
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

            #endregion

            #region Password

            await page.TypeAsync("input[name='password']", _data.Password);
            await page.TypeAsync("input[name='password_retry']", _data.Password);

            #endregion

            #region Phone
       
            const string selPhone = "input[type=tel]";
            var elPhone = await page.QuerySelectorAsync(selPhone);
            if (elPhone != null)
            {
                await page.ClickAsync(selPhone);
                await page.EvaluateFunctionAsync("function() {" + $"document.querySelector('{selPhone}').value = ''" + "}");
                await page.TypeAsync(selPhone, _data.Phone);
                await page.WaitForTimeoutAsync(300);
            }
            else
            {
                Log.Error("Phone input not found");
            }

            #endregion
        }
    }
}
