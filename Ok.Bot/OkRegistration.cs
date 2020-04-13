using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok.Bot
{
    public class OkRegistration : IBot
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(OkRegistration));
        private readonly IAccountData _data;
        private readonly ISmsService _smsService;
        private string _requestId;
        private readonly IChromiumSettings _chromiumSettings;
        private readonly string _okProxy = System.Configuration.ConfigurationManager.AppSettings[nameof(_okProxy)];
        

        public OkRegistration(IAccountData data, ISmsService smsService, IChromiumSettings chromiumSettings)
        {
            _data = data;
            _data.Domain = "ok.ru";
            _smsService = smsService;
            _chromiumSettings = chromiumSettings;
            _chromiumSettings.Proxy = _okProxy;
        }

        public async Task<IAccountData> Registration(CountryCode countryCode)
        {
            try
            {
                _data.PhoneCountryCode = Enum.GetName(typeof(CountryCode), countryCode)?.ToUpper();
                Log.Info($"Registration data: {JsonConvert.SerializeObject(_data)}");
                var phoneNumberRequest = await _smsService.GetPhoneNumber(countryCode, ServiceCode.Vk);
                //var phoneNumberRequest = new PhoneNumberRequest { Id = "444", Phone = "79777197334" };
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
    }
}
