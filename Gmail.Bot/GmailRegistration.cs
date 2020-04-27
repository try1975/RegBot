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
    public partial class GmailRegistration : RegistrationBot.Bot
    {
        #region init
        #region fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(GmailRegistration));
        public const string RegistrationUrl = @"https://accounts.google.com/signup/v2/webcreateaccount?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&ltmpl=default&gmb=exp&biz=false&flowName=GlifWebSignIn&flowEntry=SignUp";
        #endregion

        public GmailRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings) : base(data, smsService, chromiumSettings)
        {
            _chromiumSettings.Proxy = _chromiumSettings.GetProxy(GetServiceCode());
        }

        #region infra
        protected override ServiceCode GetServiceCode() => ServiceCode.Gmail;

        protected override async Task StartRegistration(Page page)
        {
            //await page.GoToAsync(GetRegistrationUrl());
            await RegistrateByPhone(page);
        }

        protected override string GetRegistrationUrl() => RegistrationUrl;
        #endregion 
        #endregion

        private async Task RegistrateByPhone(Page page)
        {

            await FillRegistrationData(page);
            await page.WaitForTimeoutAsync(2000);
            
            await page.TypeAsync("input#phoneNumberId", _data.Phone, _typeOptions);
            await page.WaitForTimeoutAsync(1000);
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
                return;
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

        #region Steps

        private async Task FillName(Page page)
        {
            var eFirstname = await page.QuerySelectorAsync("input[name=firstName]");
            await eFirstname.TypeAsync(_data.Firstname, _typeOptions);
            var eLastname = await page.QuerySelectorAsync("input[name=lastName]");
            await eLastname.TypeAsync(_data.Lastname, _typeOptions);
        }

        private async Task FillPassword(Page page)
        {
            var ePassword = await page.QuerySelectorAsync("input[name=Passwd]");
            await ePassword.TypeAsync(_data.Password, _typeOptions);
            var ePasswordConfirm = await page.QuerySelectorAsync("input[name=ConfirmPasswd]");
            await ePasswordConfirm.TypeAsync(_data.Password, _typeOptions);
        }

        private async Task FillRegistrationData(Page page)
        {
            await FillName(page);
            await FillPassword(page);

            if (string.IsNullOrEmpty(_data.AccountName))
            {
                _data.AccountName = $"{_data.Firstname.ToLower()}.{_data.Lastname.ToLower()}";
            }
            const string selLogin = "input[name=Username]";
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
        #endregion
    }
}
