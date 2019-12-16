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

        public async Task<IAccountData> Registration(CountryCode countryCode = CountryCode.RU)
        {
            try
            {
                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, ServiceCode.MailRu);
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

                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    //await page.EmulateAsync(Puppeteer.Devices[DeviceDescriptorName.IPhone6]);
                    await FillRegistrationData(page);
                    await page.ClickAsync("div.b-form__control>button");

                    // TODO check captcha
                    // check phone call
                    await page.WaitForTimeoutAsync(500);
                    var phoneCall = await page.QuerySelectorAsync("#callui-container");
                    //if (phoneCall == null) await page.WaitForTimeoutAsync(120000);
                    if (phoneCall != null)
                    {
                        Thread.Sleep(1000);
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

        public static async Task<bool> SendEmail(string to, string subject, string[] text, Page page)
        {

            try
            {
                var typeOptions = new TypeOptions { Delay = 50 };
                var selNewLetter = "span.compose-button__txt";
                if (await page.QuerySelectorAsync(selNewLetter) == null) selNewLetter = "a[data-name=compose] span";
                await page.ClickAsync(selNewLetter);
                await page.WaitForTimeoutAsync(1500);
                var selTo = "div[data-type=to] input";
                if (await page.QuerySelectorAsync(selTo) == null) selTo = "div[data-blockid='compose_to']";
                await page.ClickAsync(selTo);
                await page.TypeAsync(selTo, to, typeOptions);

                var selSubject = "input[name=Subject]";
                await page.ClickAsync("label[data-for=Subject]") ;
                await page.TypeAsync(selSubject, subject, typeOptions);
                var selText = "div[role=textbox]";
                if (await page.QuerySelectorAsync(selText) == null) {
                    var elText = await page.QuerySelectorAsync("span.mceEditor iframe");
                    var frame = await elText.ContentFrameAsync();
                    var elBody = await frame.QuerySelectorAsync("body");
                    await elBody.TypeAsync(string.Join(Environment.NewLine, text), typeOptions);
                }
                else {
                    await page.ClickAsync(selText);
                    await page.TypeAsync(selText, string.Join(Environment.NewLine, text), typeOptions);
                }
                // or CTRL+ENTER 
                
                var selSend = "span[data-title-shortcut='Ctrl+Enter']";
                if (await page.QuerySelectorAsync(selSend)==null) selSend = "div[data-name=send]";
                await page.ClickAsync(selSend);
                await page.WaitForNavigationAsync();

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
                var navigationOptions = new NavigationOptions
                {
                    WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded}
                };
                await page.WaitForNavigationAsync(navigationOptions);
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

            //await page.TypeAsync("span.b-email__name>input[type='email']", _data.AccountName);
            //const string defaultDomain = "mail.ru";
            //if (string.IsNullOrEmpty(_data.Domain))
            //{
            //    _data.Domain = defaultDomain;
            //}

            //if (!_data.Domain.ToLower().Equals(defaultDomain))
            //{
            //    //select domain
            //    await page.ClickAsync("span.b-email__domain span");
            //    await page.ClickAsync($"a[data-text='@{_data.Domain}']");
            //}

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

        public static string GetRegistrationUrl()
        {
            return @"https://account.mail.ru/signup";
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
