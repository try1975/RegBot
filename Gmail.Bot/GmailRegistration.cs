using AccountData.Service;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace Gmail.Bot
{
    public class GmailRegistration : IBot
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GmailRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;
        //private readonly string _gmailProxy = System.Configuration.ConfigurationManager.AppSettings[nameof(_gmailProxy)];


        public GmailRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = ServiceDomains.GetDomain(ServiceCode.Gmail);
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            //_chromiumSettings.Proxy = _gmailProxy;
            _chromiumSettings.Proxy = _chromiumSettings.GetProxy(ServiceCode.Gmail);
        }

        public async Task<IAccountData> Registration(CountryCode countryCode = CountryCode.RU)
        {
            try
            {
                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, ServiceCode.Gmail);
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

                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
                using (var page = await browser.NewPageAsync())
                {
                    await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
                    await FillRegistrationData(page, countryCode);
                    await page.WaitForTimeoutAsync(2000);
                    await page.TypeAsync("input#phoneNumberId", _data.Phone);
                    await page.ClickAsync("div#gradsIdvPhoneNext span>span");

                    // check phone accepted
                    try
                    {
                        await page.WaitForNavigationAsync(new NavigationOptions { Timeout = 2000 });
                    }
                    catch (Exception exception)
                    {
                        Log.Debug(exception);
                        await _smsService.SetNumberFail(_requestId);
                        _data.ErrMsg = BotMessages.PhoneNumberNotAcceptMessage;
                        return _data;
                    }

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
                        await page.TypeAsync("input#day", $"{_data.BirthDate.Day}");
                        await page.ClickAsync("select#month");
                        await page.SelectAsync("select#month", $"{_data.BirthDate.Month}");
                        await page.TypeAsync("input#year", $"{_data.BirthDate.Year}");

                        await page.ClickAsync("select#gender");
                        var gender = 3;
                        if (_data.Sex == SexCode.Male) gender = 1;
                        if (_data.Sex == SexCode.Female) gender = 2;
                        await page.SelectAsync("select#gender", $"{gender}");

                        await page.ClickAsync("div[role=button] span>span");

                        await page.WaitForNavigationAsync();
                        await page.WaitForTimeoutAsync(2000);
                        await page.ClickAsync("div[data-button-id-prefix=phoneUsage] button[type=button]");

                        await page.WaitForNavigationAsync();
                        await page.WaitForTimeoutAsync(2000);

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
            await page.GoToAsync(GetRegistrationUrl());

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

            #endregion

            #region Password

            await page.TypeAsync("input[name=Passwd]", _data.Password);
            await page.TypeAsync("input[name=ConfirmPasswd]", _data.Password);

            #endregion

            if (await EmailAlreadyRegistered(_data.AccountName, page))
            {
                const string selAltEmail = "ul#usernameList li";
                var elAltEmail = await page.QuerySelectorAsync(selAltEmail);
                await elAltEmail.ClickAsync();
                var elUsername = await page.QuerySelectorAsync(selLogin);
                var accountName = await elUsername.EvaluateFunctionAsync<string>("node => node.value");
                _data.AccountName = accountName;
            }
        }

        public static string GetRegistrationUrl()
        {
            return @"https://accounts.google.com/signup/v2/webcreateaccount?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&ltmpl=default&gmb=exp&biz=false&flowName=GlifWebSignIn&flowEntry=SignUp";
        }

        public async static Task<bool> EmailAlreadyRegistered(string accountName, Page page, string path = null)
        {
            try
            {
                if (path != null)
                {
                    var _data = new AccountDataGenerator(path).GetRandom();
                    await page.TypeAsync("input[name=firstName]", _data.Firstname);
                    await page.TypeAsync("input[name=lastName]", _data.Lastname);

                    await page.TypeAsync("input[name=Passwd]", _data.Password);
                    await page.TypeAsync("input[name=ConfirmPasswd]", _data.Password);
                }

                const string selLogin = "input[name=Username]";
                await page.ClickAsync(selLogin);
                await page.EvaluateFunctionAsync("function() {" + $"document.querySelector('{selLogin}').value = ''" + "}");
                await page.TypeAsync(selLogin, accountName);

                await page.ClickAsync("div#accountDetailsNext span>span");

                await page.WaitForTimeoutAsync(2000);
                const string selAltEmail = "ul#usernameList li";
                var elAltEmail = await page.QuerySelectorAsync(selAltEmail);
                if (!(elAltEmail != null && await elAltEmail.IsIntersectingViewportAsync())) return false;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            return true;
        }
    }
}
