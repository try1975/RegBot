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


namespace Ig.Bot
{
    public class InstagramRegistration : RegistrationBot.Bot
    {
        #region fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(InstagramRegistration));
        public const string RegistrationUrl = @"https://www.instagram.com/accounts/emailsignup/";
        #endregion

        public InstagramRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings) : base(data, smsService, chromiumSettings)
        {
            _chromiumSettings.Proxy = _chromiumSettings.GetProxy(ServiceCode.Ok);
        }

        protected override ServiceCode GetServiceCode() => ServiceCode.Instagram;

        protected override string GetRegistrationUrl() => RegistrationUrl;

        protected override async Task StartRegistration(Page page)
        {
            await RegistrateByPhone(page);
        }

        private async Task RegistrateByPhone(Page page)
        {
            await FillPhone(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillAccount(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillBirtDate(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillSms(page);
            await page.WaitForNavigationAsync();
            _data.Success = true;
        }

        private async Task FillPhone(Page page)
        {
            await page.WaitForTimeoutAsync(500);
            var ePhone = await page.QuerySelectorAsync("input[name='emailOrPhone'");
            await ePhone.ClickAsync(new ClickOptions { ClickCount = 3 });
            await ePhone.TypeAsync(_data.Phone, _typeOptions);
            await page.WaitForTimeoutAsync(500);
        }

        private async Task FillAccount(Page page)
        {
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            var eFullname = await page.QuerySelectorAsync("input[name='fullName'");
            await eFullname.ClickAsync();
            await eFullname.TypeAsync($"{_data.Firstname} {_data.Lastname}", _typeOptions);

            var eUsername = await page.QuerySelectorAsync("input[name='username'");
            await eUsername.ClickAsync();
            await eUsername.TypeAsync(_data.AccountName, _typeOptions);

            var ePassword = await page.QuerySelectorAsync("input[name='password'");
            await ePassword.ClickAsync();
            await ePassword.TypeAsync(_data.Password, _typeOptions);

            await page.WaitForTimeoutAsync(500);
            await ClickSubmit(page);
            await page.WaitForTimeoutAsync(1000);
        }

        private async Task FillBirtDate(Page page)
        {
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            try
            {
                var elements = await page.QuerySelectorAllAsync("select[title]");
                var eMonth = elements[0];
                await eMonth.SelectAsync($"{_data.BirthDate.Month}");
                await page.WaitForTimeoutAsync(500);

                var eDay = elements[1];
                await eDay.SelectAsync($"{_data.BirthDate.Day}");
                await page.WaitForTimeoutAsync(500);

                var eYear = elements[2];
                await eYear.SelectAsync($"{_data.BirthDate.Year}");
                await page.WaitForTimeoutAsync(1000);

                var buttons = await page.QuerySelectorAllAsync("button[type='button']");
                await buttons[1].ClickAsync();
                await page.WaitForTimeoutAsync(1000);
            }
            catch (Exception exception)
            {
                Log.Error($"{exception}");
                _data.ErrMsg = $"{exception}";
            }
        }

        private async Task FillSms(Page page)
        {
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            
            var eSmsInput = await page.WaitForSelectorAsync("input[name='confirmationCode']");
            if (eSmsInput != null)
            {
                var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                if (phoneNumberValidation != null)
                {
                    await _smsService.SetSmsValidationSuccess(_requestId);
                    // enter sms code
                    await eSmsInput.TypeAsync(phoneNumberValidation.Code, _typeOptions);
                    var buttons = await page.QuerySelectorAsync("button[type='button']");
                    await buttons.ClickAsync();
                    await page.WaitForTimeoutAsync(500);
                }
                else
                {
                    _data.ErrMsg = BotMessages.PhoneNumberNotRecieveSms;
                    await _smsService.SetNumberFail(_requestId);
                }
            }
        }

        private static async Task ClickSubmit(Page page)
        {
            var elSignUp = await page.QuerySelectorAsync("button[type=submit]");

            await elSignUp.ClickAsync();
            await page.WaitForTimeoutAsync(500);
        }
    }
}
