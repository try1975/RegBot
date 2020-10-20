using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Threading.Tasks;

namespace Tw.Bot
{
    public class TwitterRegistration : RegistrationBot.Bot
    {
        #region fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(TwitterRegistration));
        public const string RegistrationUrl = @"https://twitter.com/i/flow/signup";
        #endregion

        public TwitterRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings) : base(data, smsService, chromiumSettings)
        {
            _chromiumSettings.Proxy = _chromiumSettings.GetProxy(ServiceCode.Ok);
        }

        protected override ServiceCode GetServiceCode() => ServiceCode.Twitter;
        protected override string GetRegistrationUrl() => RegistrationUrl;

        protected override async Task StartRegistration(Page page)
        {
            await RegistrateByPhone(page);
        }

        private async Task RegistrateByPhone(Page page)
        {
            await FillPhone(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillBirtDate(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillAccount(page);

            await page.ClickAsync("div[role='button']");
            await page.WaitForTimeoutAsync(1500);

            var buttons = await page.QuerySelectorAllAsync("div[role='button']");
            if (buttons.Length >= 2) await buttons[1].ClickAsync();
            await page.WaitForTimeoutAsync(1500);
            buttons = await page.QuerySelectorAllAsync("div[role='button']");
            if (buttons.Length >= 4) await buttons[3].ClickAsync();
            buttons = await page.QuerySelectorAllAsync("div[role='button']");
            if (buttons.Length >= 6) await buttons[5].ClickAsync();

            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            await FillSms(page);
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
            buttons = await page.QuerySelectorAllAsync("div[role='button']");
            if (buttons.Length >= 2) await buttons[1].ClickAsync();
            await page.WaitForTimeoutAsync(500);

            try
            {
                var ePassword = await page.QuerySelectorAsync("input[name='password'");
                await ePassword.ClickAsync();
                await ePassword.TypeAsync(_data.Password, _typeOptions);

                await page.ClickAsync("div[role='button']");
                await page.WaitForTimeoutAsync(1500);
                await page.ClickAsync("div[role='button']");
                await page.WaitForTimeoutAsync(1500);
                await page.ClickAsync("div[role='button']");
                await page.WaitForTimeoutAsync(1500);
                await page.ClickAsync("div[role='button']");
                await page.WaitForTimeoutAsync(1500);
                await page.ClickAsync("div[role='button']");
                await page.WaitForTimeoutAsync(1500);
                await page.ClickAsync("div[role='button']");
                await page.WaitForTimeoutAsync(2500);

                //await page.WaitForNavigationAsync();
                _data.Success = true;

            }
            catch (Exception exception)
            {

                Log.Error($"{exception}");
                _data.ErrMsg = $"{exception}";
            }

        }

        private async Task FillPhone(Page page)
        {
            await page.WaitForTimeoutAsync(500);
            var ePhone = await page.QuerySelectorAsync("input[name='phone_number'");
            await ePhone.ClickAsync(new ClickOptions { ClickCount = 3 });
            await ePhone.TypeAsync(_data.Phone, _typeOptions);
            await page.WaitForTimeoutAsync(500);
        }

        private async Task FillAccount(Page page)
        {
            var eFullname = await page.QuerySelectorAsync("input[name='name'");
            await eFullname.ClickAsync();
            await eFullname.TypeAsync($"{_data.Firstname} {_data.Lastname}", _typeOptions);

            //var eUsername = await page.QuerySelectorAsync("input[name='username'");
            //await eUsername.ClickAsync();
            //await eUsername.TypeAsync(_data.AccountName, _typeOptions);

            //var ePassword = await page.QuerySelectorAsync("input[name='password'");
            //await ePassword.ClickAsync();
            //await ePassword.TypeAsync(_data.Password, _typeOptions);

            await page.WaitForTimeoutAsync(500);

        }

        private async Task FillBirtDate(Page page)
        {
            try
            {
                var elements = await page.QuerySelectorAllAsync("select[class]");
                var eMonth = elements[0];
                await eMonth.SelectAsync($"{_data.BirthDate.Month}");
                await page.WaitForTimeoutAsync(500);

                var eDay = elements[1];
                await eDay.SelectAsync($"{_data.BirthDate.Day}");
                await page.WaitForTimeoutAsync(500);

                var eYear = elements[2];
                await eYear.SelectAsync($"{_data.BirthDate.Year}");
                await page.WaitForTimeoutAsync(500);
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

            var eSmsInput = await page.WaitForSelectorAsync("input[name='verfication_code']");
            if (eSmsInput != null)
            {
                var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                if (phoneNumberValidation != null)
                {
                    await _smsService.SetSmsValidationSuccess(_requestId);
                    // enter sms code
                    await eSmsInput.TypeAsync(phoneNumberValidation.Code, _typeOptions);
                }
                else
                {
                    _data.ErrMsg = BotMessages.PhoneNumberNotRecieveSms;
                    await _smsService.SetNumberFail(_requestId);
                }
            }
        }
    }
}
