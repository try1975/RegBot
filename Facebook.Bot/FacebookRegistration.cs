using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;

namespace Facebook.Bot
{
    public class FacebookRegistration : IBot
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FacebookRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;

        public FacebookRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = "facebook.com";
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
        }


        public async Task<IAccountData> Registration(CountryCode countryCode)
        {
            try
            {
                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, ServiceCode.Facebook);
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

                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    await FillRegistrationData(page);
                    await page.ClickAsync("button[type=submit]");

                    // check phone accepted
                    try
                    {
                        await page.WaitForNavigationAsync();
                    }
                    catch (Exception exception)
                    {
                        Log.Debug(exception);
                        await _smsService.SetNumberFail(_requestId);
                        _data.ErrMsg = BotMessages.PhoneNumberNotAcceptMessage;
                        return _data;
                    }

                    var phoneNumberValidation = await _smsService.GetSmsValidation(_requestId);
                    Log.Info($"phoneNumberValidation: {JsonConvert.SerializeObject(phoneNumberValidation)}");
                    if (phoneNumberValidation != null)
                    {
                        await _smsService.SetSmsValidationSuccess(_requestId);
                        const string selSmsCode = "input#code_in_cliff";
                        await page.ClickAsync(selSmsCode);
                        await page.TypeAsync(selSmsCode, phoneNumberValidation.Code);
                        await page.ClickAsync("div.uiInterstitialContent button");
                        await page.WaitForSelectorAsync("div._t a", new WaitForSelectorOptions { Timeout = 20000 });
                        await page.ClickAsync("div._t a");

                        await page.WaitForNavigationAsync();
                        await page.WaitForTimeoutAsync(3000);
                        _data.Success = true;
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

        private async Task FillRegistrationData(Page page)
        {
            await page.GoToAsync("https://www.facebook.com/?_rdc=1&_rdr");

            #region Name
            await page.WaitForSelectorAsync("input[name=firstname]", new WaitForSelectorOptions { Visible = true });
            await page.ClickAsync("input[name=firstname]");
            await page.TypeAsync("input[name=firstname]", _data.Firstname);
            await page.TypeAsync("input[name=lastname]", _data.Lastname);


            #endregion

            await page.TypeAsync("input[name=reg_email__]", _data.Phone);
            await page.TypeAsync("input[name=reg_passwd__]", _data.Password);


            await page.ClickAsync("select#day");
            await page.SelectAsync("select#day", $"{_data.BirthDate.Day}");
            await page.ClickAsync("select#month");
            await page.SelectAsync("select#month", $"{_data.BirthDate.Month}");
            await page.ClickAsync("select#year");
            await page.SelectAsync("select#year", $"{_data.BirthDate.Year}");


            if (_data.Sex == SexCode.Female) await page.ClickAsync("input[name=sex][value='1']");
            if (_data.Sex == SexCode.Male) await page.ClickAsync("input[name=sex][value='2']");
        }
    }
}
