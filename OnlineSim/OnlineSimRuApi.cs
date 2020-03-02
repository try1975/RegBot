using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using Newtonsoft.Json;

namespace OnlineSimRu
{
    public class OnlineSimRuApi : ISmsService
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(OnlineSimRuApi));

        private const string BaseUrl = "https://onlinesim.ru/api";
        private readonly string _apiKeyOnlineSimRu = System.Configuration.ConfigurationManager.AppSettings[nameof(_apiKeyOnlineSimRu)];
        private readonly HttpClient _apiHttpClient;
        private readonly string _endpointGetNum;
        private readonly string _endpointGetState;
        private readonly string _endpointSetOperationOk;
        private readonly string _endpointGetNumberStats;

        private readonly Dictionary<CountryCode, string> _countries = PhoneServiceStore.CountryPrefixes;
        private readonly Dictionary<ServiceCode, string> _services = new Dictionary<ServiceCode, string>();

        #endregion

        public OnlineSimRuApi()
        {
            // ReSharper disable once StringLiteralTypo
            const string apiKeyParameterName = "apikey";
            _apiHttpClient = new HttpClient(new LoggingHandler());
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _endpointGetNum = $"{BaseUrl}/getNum.php?{apiKeyParameterName}={_apiKeyOnlineSimRu}";
            _endpointGetState = $"{BaseUrl}/getState.php?{apiKeyParameterName}={_apiKeyOnlineSimRu}";
            _endpointSetOperationOk = $"{BaseUrl}/setOperationOk.php?{apiKeyParameterName}={_apiKeyOnlineSimRu}";
            _endpointGetNumberStats = $"{BaseUrl}/getNumbersStats.php?{apiKeyParameterName}={_apiKeyOnlineSimRu}";

            _services[ServiceCode.MailRu] = "MailRu";
            _services[ServiceCode.Yandex] = "Yandex";
            _services[ServiceCode.Gmail] = "Google";
            _services[ServiceCode.Other] = "other";
            _services[ServiceCode.Facebook] = "facebook";
            _services[ServiceCode.Vk] = "VKcom";
            _services[ServiceCode.Ok] = "Odklru";
        }

        #region OnlineSim API

        private async Task<OnlineSimRuStatResponse> GetNumbersStats(string country)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetNumberStats}&country={country}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetNum)}... {result}");
                return JsonConvert.DeserializeObject<OnlineSimRuStatResponse>(result);
            }
        }

        private async Task<string> GetNum(string service, string country)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetNum}&service={service}&country={country}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetNum)}... {result}");
                var getNumResponse = JsonConvert.DeserializeObject<GetNumResponse>(result);
                return !getNumResponse.response.Equals("1") ? string.Empty : getNumResponse.tzid.ToString();
            }
        }

        private async Task<GetStateResponseItem> GetState(string id)
        {
            try
            {
                using (var response = await _apiHttpClient.GetAsync($"{_endpointGetState}&tzid={id}"))
                {
                    if (!response.IsSuccessStatusCode) return null;
                    var result = await response.Content.ReadAsStringAsync();
                    Log.Debug($"{nameof(GetState)}... {result}");

                    var getStateResponseItems = JsonConvert.DeserializeObject<List<GetStateResponseItem>>(result);
                    return getStateResponseItems.FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Log.Error($"{exception}");
            }
            return null;
        }

        private async Task SetOperationOk(string id)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointSetOperationOk}&tzid={id}"))
            {
                if (!response.IsSuccessStatusCode) return;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(SetOperationOk)}... {result}");
            }
        }

        private async Task SetOperationFail(string id)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointSetOperationOk}&tzid={id}&ban=1"))
            {
                if (!response.IsSuccessStatusCode) return;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(SetOperationFail)}... {result}");
            }
        }

        #endregion

        public async Task<PhoneNumberRequest> GetPhoneNumber(CountryCode countryCode = CountryCode.RU, ServiceCode serviceCode = ServiceCode.MailRu)
        {
            Log.Debug($"Call {nameof(GetPhoneNumber)}");
            if (!_countries.ContainsKey(countryCode))
            {
                Log.Error($"{nameof(OnlineSimRuApi)} not available for country {Enum.GetName(typeof(CountryCode), countryCode)}");
                return null;
            }
            if (!_services.ContainsKey(serviceCode))
            {
                Log.Error($"{nameof(OnlineSimRuApi)} not available for service {Enum.GetName(typeof(ServiceCode), serviceCode)}");
                return null;
            }
            var country = _countries[countryCode];
            var service = _services[serviceCode];
            var id = await GetNum(service, country);

            if (string.IsNullOrEmpty(id)) { return null; }
            var getStateResponseItem = await GetState(id);
            if (getStateResponseItem == null) { return null; }

            //var length = 11 - _countries[countryCode].Length;
            //var phone = getStateResponseItem.number.Substring(getStateResponseItem.number.Length - length);
            var phone = getStateResponseItem.number;
            var activeSeconds = 900;
            return new PhoneNumberRequest { Id = id, Phone = phone, Created = DateTime.UtcNow, ActiveSeconds = activeSeconds, RemainSeconds = activeSeconds };
        }

        public async Task<PhoneNumberValidation> GetSmsValidation(string id)
        {
            Log.Debug($"Call {nameof(GetSmsValidation)}");
            var getStateResponseItem = await GetState(id);
            var tryCount = 0;
            // if !response.IsSuccessStatusCode
            while (getStateResponseItem == null && tryCount < 3)
            {
                getStateResponseItem = await GetState(id);
                tryCount += 1;
            }
            if (getStateResponseItem != null && !string.IsNullOrEmpty(getStateResponseItem.msg))
            {
                return new PhoneNumberValidation { Code = getStateResponseItem.msg };
            }
            tryCount = 0;
            while (getStateResponseItem != null && string.IsNullOrEmpty(getStateResponseItem.msg) && tryCount < 100)
            {
                Thread.Sleep(1000);
                getStateResponseItem = await GetState(id);
                if (getStateResponseItem != null && !string.IsNullOrEmpty(getStateResponseItem.msg))
                {
                    return new PhoneNumberValidation { Code = getStateResponseItem.msg };
                }
                tryCount += 1;
            }
            return null;
        }

        public async Task SetSmsValidationSuccess(string id)
        {
            await SetOperationOk(id);
        }

        public async Task SetNumberFail(string id)
        {
            Log.Debug($"Call {nameof(SetNumberFail)}");
            await SetOperationFail(id);
        }

        public async Task<List<SmsServiceInfo>> GetInfo()
        {
            var list = new List<SmsServiceInfo>();
            var smsServiceCode = Enum.GetName(typeof(SmsServiceCode), SmsServiceCode.OnlineSimRu);
            var mailRu = Enum.GetName(typeof(ServiceCode), ServiceCode.MailRu);
            var yandex = Enum.GetName(typeof(ServiceCode), ServiceCode.Yandex);
            var gmail = Enum.GetName(typeof(ServiceCode), ServiceCode.Gmail);
            foreach (var country in _countries)
            {
                var countryCode = Enum.GetName(typeof(CountryCode), country.Key);
                var onlineSimRuStatResponse = await GetNumbersStats(country.Value);
                if (onlineSimRuStatResponse == null) continue;
                if (!onlineSimRuStatResponse.enabled) continue;
                if (onlineSimRuStatResponse.services.mailru?.count > 0)
                {
                    list.Add(new SmsServiceInfo
                    {
                        SmsServiceCode = smsServiceCode,
                        CountryCode = countryCode,
                        MailServiceCode = mailRu,
                        NumberCount = onlineSimRuStatResponse.services.mailru.count.Value,
                        Price = onlineSimRuStatResponse.services.mailru.price
                    });
                }
                if (onlineSimRuStatResponse.services.yandex?.count > 0)
                {
                    list.Add(new SmsServiceInfo
                    {
                        SmsServiceCode = smsServiceCode,
                        CountryCode = countryCode,
                        MailServiceCode = yandex,
                        NumberCount = onlineSimRuStatResponse.services.yandex.count.Value,
                        Price = onlineSimRuStatResponse.services.yandex.price
                    });
                }
                if (onlineSimRuStatResponse.services.google?.count > 0)
                {
                    list.Add(new SmsServiceInfo
                    {
                        SmsServiceCode = smsServiceCode,
                        CountryCode = countryCode,
                        MailServiceCode = gmail,
                        NumberCount = onlineSimRuStatResponse.services.google.count.Value,
                        Price = onlineSimRuStatResponse.services.google.price
                    });
                }
            }
            return list;
        }

        public async Task<PhoneNumberValidation> GetSmsOnes(string id)
        {
            Log.Debug($"Call {nameof(GetSmsOnes)}");
            var getStateResponseItem = await GetState(id);
            if (getStateResponseItem != null && !string.IsNullOrEmpty(getStateResponseItem.msg))
            {
                return new PhoneNumberValidation { Code = getStateResponseItem.msg };
            }
            return null;
        }
    }
}
