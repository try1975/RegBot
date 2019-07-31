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

namespace Gmail.Bot
{
    public class GmailRegistration : IBot
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GmailRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly string _chromiumPath;

        public GmailRegistration(IAccountData data, ISmsService smsService, string chromiumPath)
        {
            _data = data;
            _data.Domain = "gmail.com";
            _smsService = smsService;
            if (string.IsNullOrEmpty(chromiumPath)) chromiumPath = Environment.CurrentDirectory;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-662092\\chrome-win\\chrome.exe");
            _chromiumPath = chromiumPath;
        }

        public async Task<IAccountData> Registration(CountryCode countryCode = CountryCode.RU)
        {
            try
            {
                var options = new LaunchOptions
                {
                    Headless = false,
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

                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, MailServiceCode.Gmail);
                //var phoneNumberRequest = new PhoneNumberRequest {Id="444", Phone = "79619361800"};
                if (phoneNumberRequest == null)
                {
                    _data.ErrMsg = BotMessages.NoPhoneNumberMessage;
                    return _data;
                }
                Log.Info($"phoneNumberRequest: {JsonConvert.SerializeObject(phoneNumberRequest)}");
                _requestId = phoneNumberRequest.Id;
                _data.Phone = phoneNumberRequest.Phone.Trim();
                if (!_data.Phone.StartsWith("+")) _data.Phone = $"+{_data.Phone}";

                using (var browser = await Puppeteer.LaunchAsync(options))
                using (var page = await browser.NewPageAsync())
                {
                    await FillRegistrationData(page, countryCode);
                    await page.WaitForTimeoutAsync(5000);
                    var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                    Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                    if (phoneNumberValidation != null)
                    {
                        await _smsService.SetSmsValidationSuccess(_requestId);
                        //input#code
                        await page.TypeAsync("input#code", phoneNumberValidation.Code);
                        // click div[role=button] span>span
                        await page.ClickAsync("div[role=button] span>span");

                        await page.WaitForNavigationAsync();
                        await page.WaitForTimeoutAsync(2000);
                        //input#day
                        await page.TypeAsync("input#day", $"{_data.BirthDate.Day}");
                        //select#month
                        await page.ClickAsync("select#month");
                        // <select class="N9rVke" jsname="sC6rpf" id="month" aria-labelledby="month-label"><option value=""></option><option value="1">Январь</option><option value="2">Февраль</option><option value="3">Март</option><option value="4">Апрель</option><option value="5">Май</option><option value="6">Июнь</option><option value="7">Июль</option><option value="8">Август</option><option value="9">Сентябрь</option><option
                        await page.SelectAsync("select#month", $"{_data.BirthDate.Month}");
                        //$('select#month').value='1'
                        //await page.ClickAsync(selMonth);
                        //input#year
                        await page.TypeAsync("input#year", $"{_data.BirthDate.Year}");

                        //select#gender
                        await page.ClickAsync("select#gender");
                        //<select class="N9rVke" jsname="sC6rpf" id="gender" aria-labelledby="gender-label"><option value=""></option><option value="2">Женский</option><option value="1">Мужской</option><option value="3">Не указан</option><option value="4">Дополнительно</option></select>
                        var gender = 3;
                        if (_data.Sex == SexCode.Male) gender = 1;
                        if (_data.Sex == SexCode.Female) gender = 2;
                        await page.SelectAsync("select#gender", $"{gender}");

                        //click div[role=button] span>span
                        await page.ClickAsync("div[role=button] span>span");

                        await page.WaitForNavigationAsync();
                        await page.WaitForTimeoutAsync(2000);
                        //click div[data-button-id-prefix=phoneUsage] button[type=button]
                        await page.ClickAsync("div[data-button-id-prefix=phoneUsage] button[type=button]");

                        await page.WaitForNavigationAsync();
                        await page.WaitForTimeoutAsync(2000);
                        //div[role=presentation] div[role=button] svg

                        for (var i = 0; i < 5; i++)
                        {
                            await page.ClickAsync("div[role=presentation] div[role=button] svg");
                            await page.WaitForTimeoutAsync(500);
                            var termButton = await page.QuerySelectorAsync("div#termsofserviceNext");
                            var termButtonVisible = termButton != null && await termButton.IsIntersectingViewportAsync();
                            if (termButtonVisible)
                            {
                                await page.ClickAsync("div#termsofserviceNext");
                                _data.Success = true;
                                await page.WaitForNavigationAsync();
                                break;
                            }
                        }
                        //await page.WaitForTimeoutAsync(8000);


                    }
                    else await _smsService.SetNumberFail(_requestId);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _data.ErrMsg = exception.Message;
                await _smsService.SetNumberFail(_requestId);
            }

            return _data;
        }

        private async Task FillRegistrationData(Page page, CountryCode countryCode)
        {
            await page.GoToAsync("https://accounts.google.com/signup/v2/webcreateaccount?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&ltmpl=default&gmb=exp&biz=false&flowName=GlifWebSignIn&flowEntry=SignUp");

            #region Name

            await page.TypeAsync("input[name=firstName]", _data.Firstname);
            await page.TypeAsync("input[name=lastName]", _data.Lastname);

            #endregion

            #region Login

            if (string.IsNullOrEmpty(_data.AccountName))
            {
                _data.AccountName = $"{_data.Firstname.ToLower()}.{_data.Lastname.ToLower()}";
            }

            const string selLogin = "input[name=Username]";
            await page.ClickAsync(selLogin);
            await page.EvaluateFunctionAsync("function() {" + $"document.querySelector('{selLogin}').value = ''" + "}");
            await page.TypeAsync(selLogin, _data.AccountName);
            //<ul id="usernameList"
            #endregion

            #region Password

            await page.TypeAsync("input[name=Passwd]", _data.Password);
            await page.TypeAsync("input[name=ConfirmPasswd]", _data.Password);

            #endregion

            await page.ClickAsync("div#accountDetailsNext span>span");
            //check div[aria-live=assertive] and select alternate account name



            await page.WaitForNavigationAsync();
            await page.WaitForTimeoutAsync(2000);
            await page.TypeAsync("input#phoneNumberId", _data.Phone);
            await page.ClickAsync("div#gradsIdvPhoneNext span>span");
            // check div[aria-live=assertive] 
        }

        private async Task<WebSocket> WebSocketFactory(Uri url, IConnectionOptions options,
            CancellationToken cancellationToken)
        {
            var ws = new System.Net.WebSockets.Managed.ClientWebSocket();
            await ws.ConnectAsync(url, (CancellationToken) cancellationToken);
            return ws;
        }
    }
}
