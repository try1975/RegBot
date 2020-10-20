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
using System.Threading.Tasks;

namespace RegistrationBot
{
    public abstract class Bot : IBot
    {
        #region fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(Bot));
        protected readonly IAccountData _data;
        protected readonly ISmsService _smsService;
        protected readonly IChromiumSettings _chromiumSettings;
        protected string _requestId;
        protected string _countryPrefix;
        protected static readonly TypeOptions _typeOptions = new TypeOptions { Delay = 50 };
        protected static readonly NavigationOptions _navigationOptions = new NavigationOptions
        {
            WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Load/*, WaitUntilNavigation.Networkidle2*/ }
        };
        #endregion

        protected Bot(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = ServiceDomains.GetDomain(GetServiceCode());
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            _chromiumSettings.ServiceCode = GetServiceCode();
        }

        public async Task<IAccountData> Registration(CountryCode countryCode)
        {
            try
            {
                _countryPrefix = PhoneServiceStore.CountryPrefixes[countryCode];
                if (!string.IsNullOrEmpty(await SmsServiceInit(countryCode))) return _data;
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
                using (var page = await PageInit(browser)) await StartRegistration(page);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                _data.ErrMsg = exception.Message;
            }
            return _data;
        }

        protected async Task<string> SmsServiceInit(CountryCode countryCode)
        {
            _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
            Log.Debug($"Registration data: {JsonConvert.SerializeObject(_data)}");
            if (_smsService == null)
            {
                _data.Phone = PhoneServiceStore.GetRandomPhoneNumber(countryCode);
                return _data.ErrMsg; ;
            }
            PhoneNumberRequest phoneNumberRequest = null;
            phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, GetServiceCode());
            //phoneNumberRequest = new PhoneNumberRequest { Id = "444", Phone = "79163848169" };
            if (phoneNumberRequest == null)
            {
                _data.ErrMsg = BotMessages.NoPhoneNumberMessage;
                return _data.ErrMsg;
            }
            Log.Debug($"phoneNumberRequest: {JsonConvert.SerializeObject(phoneNumberRequest)}");
            _requestId = phoneNumberRequest.Id;
            _data.Phone = phoneNumberRequest.Phone.Trim();
            if (!_data.Phone.StartsWith("+")) _data.Phone = $"+{_data.Phone}";
            //_data.Phone = _data.Phone.Substring(PhoneServiceStore.CountryPrefixes[countryCode].Length + 1);
            return _data.ErrMsg;
        }

        private async Task<Page> PageInit(Browser browser, bool isIncognito = false)
        {
            Page page;
            if (isIncognito)
            {
                var context = await browser.CreateIncognitoBrowserContextAsync();
                page = await context.NewPageAsync();
            }
            //else page = await browser.NewPageAsync();
            else page = (await browser.PagesAsync())[0];

            #region commented
            //await SetRequestHook(page);
            //var deviceDescriptorName = GetDeviceDescriptorName();
            //if (deviceDescriptorName != DeviceDescriptorName.BlackberryPlayBook)
            //{
            //    await page.EmulateAsync(Puppeteer.Devices[deviceDescriptorName]);
            //}
            #endregion
            await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
            await page.GoToAsync(GetRegistrationUrl(), _navigationOptions);
            return page;
        }

        #region abstract
        protected abstract ServiceCode GetServiceCode();

        protected abstract string GetRegistrationUrl();

        protected abstract Task StartRegistration(Page page);

        protected virtual DeviceDescriptorName GetDeviceDescriptorName()
        {
            return DeviceDescriptorName.BlackberryPlayBook;
        }
        #endregion
    }
}
