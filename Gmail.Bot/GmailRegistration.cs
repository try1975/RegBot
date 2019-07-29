using System;
using System.IO;
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
                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                //var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, MailServiceCode.Yandex);
                var phoneNumberRequest = new PhoneNumberRequest {Id="444", Phone = "9163848169"};
                if (phoneNumberRequest == null)
                {
                    _data.ErrMsg = BotMessages.NoPhoneNumberMessage;
                    return _data;
                }
                Log.Info($"phoneNumberRequest: {JsonConvert.SerializeObject(phoneNumberRequest)}");
                _requestId = phoneNumberRequest.Id;
                _data.Phone = phoneNumberRequest.Phone.Trim();
                if(!_data.Phone.StartsWith("+")) _data.Phone = $"+{_data.Phone}";

                var options = new LaunchOptions
                {
                    Headless = false,
                    ExecutablePath = _chromiumPath
                    //SlowMo = 10
                };

                using (var browser = await Puppeteer.LaunchAsync(options))
                using (var page = await browser.NewPageAsync())
                {
                    await FillRegistrationData(page);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _data.ErrMsg = exception.Message;
            }

            return _data;
        }

        private async Task FillRegistrationData(Page page)
        {
            await page.GoToAsync("https://accounts.google.com/signup/v2/webcreateaccount?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&ltmpl=default&gmb=exp&biz=false&flowName=GlifWebSignIn&flowEntry=SignUp");

            #region Name

            await page.TypeAsync("input[name=firstName]", _data.Firstname);
            await page.TypeAsync("input[name=lastName]", _data.Lastname);

            #endregion
        }
    }
}
