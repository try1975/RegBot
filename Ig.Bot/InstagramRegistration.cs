using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
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
            await eUsername.ClickAsync();
            await eUsername.TypeAsync(_data.Password, _typeOptions);

            //var eBirthday = await page.QuerySelectorAsync("input#field_birthday");
            //await eBirthday.ClickAsync();

            //var eYear = await page.QuerySelectorAsync("select.ui-datepicker-year");
            //await eYear.ClickAsync();
            //await eYear.SelectAsync($"{_data.BirthDate.Year}");

            //var eMonth = await page.QuerySelectorAsync("select.ui-datepicker-month");
            //await eMonth.ClickAsync();
            //await eMonth.SelectAsync($"{_data.BirthDate.Month - 1}");

            //var eDays = await page.QuerySelectorAllAsync("a.ui-state-default:not(.ui-priority-secondary)");
            //await eDays[_data.BirthDate.Day - 1].ClickAsync();

            //await page.WaitForTimeoutAsync(500);
            //var eSex = await page.QuerySelectorAllAsync("span.btn-group_i_t");
            //if (_data.Sex == SexCode.Male) { await eSex[0].ClickAsync(); }
            //if (_data.Sex == SexCode.Female) { await eSex[1].ClickAsync(); await eSex[1].ClickAsync(); }
            await page.WaitForTimeoutAsync(500);
            await ClickSubmit(page);
        }

        private async Task FillBirtDate(Page page)
        {
            if (!string.IsNullOrEmpty(_data.ErrMsg)) return;
        }
        private static async Task ClickSubmit(Page page)
        {
            var elSignUp = await page.QuerySelectorAsync("button[type=submit]");

            await elSignUp.ClickAsync();
            await page.WaitForTimeoutAsync(500);
        }
    }
}
