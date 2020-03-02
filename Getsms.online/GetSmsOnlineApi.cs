using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;

namespace GetSmsOnline
{
    public class GetSmsOnlineApi : ISmsService
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(GetSmsOnlineApi));

        private const string BaseUrl = "http://api.getsms.online/stubs/handler_api.php";
        private readonly string _apiKeyGetSmsOnline = System.Configuration.ConfigurationManager.AppSettings[nameof(_apiKeyGetSmsOnline)];
        private readonly HttpClient _apiHttpClient;
        private readonly string _endpointGetNumber;
        private readonly string _endpointSetStatus;
        private readonly string _endpointGetStatus;

        private readonly Dictionary<ServiceCode, string> _services = new Dictionary<ServiceCode, string>();


        #endregion

        public GetSmsOnlineApi()
        {
            _apiHttpClient = new HttpClient(new LoggingHandler());
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            const string apiKeyParameterName = "api_key";
            _endpointGetNumber = $"{BaseUrl}?action=getNumber&{apiKeyParameterName}={_apiKeyGetSmsOnline}";
            _endpointSetStatus = $"{BaseUrl}?action=setStatus&{apiKeyParameterName}={_apiKeyGetSmsOnline}";
            _endpointGetStatus = $"{BaseUrl}?action=getStatus&{apiKeyParameterName}={_apiKeyGetSmsOnline}";

            _services[ServiceCode.MailRu] = "ma";
            _services[ServiceCode.Yandex] = "ya";
            _services[ServiceCode.Gmail] = "gm/go";
            _services[ServiceCode.Other] = "or/ot";
            _services[ServiceCode.Facebook] = "fb";
            _services[ServiceCode.Vk] = "vk";
            _services[ServiceCode.Ok] = "ok";
        }

        private async Task<string> GetNumber(string service, string country)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetNumber}&service={service}&country={country}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetNumber)}... {result}");
                return result;
            }
        }

        private async Task<string> SetStatus(string id, string status)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointSetStatus}&id={id}&status={status}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(SetStatus)}... {result}");
                return result;
            }
        }

        private async Task<string> GetStatus(string id)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetStatus}&id={id}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetStatus)}... {result}");
                return result;
            }
        }

        public async Task<PhoneNumberRequest> GetPhoneNumber(CountryCode countryCode = CountryCode.RU, ServiceCode serviceCode = ServiceCode.MailRu)
        {
            Log.Debug($"Call {nameof(GetPhoneNumber)}");
            //if (!_countries.ContainsKey(countryCode))
            //{
            //    Log.Error($"{nameof(GetSmsOnlineApi)} not available for country {Enum.GetName(typeof(CountryCode), countryCode)}");
            //    return null;
            //}
            if (!_services.ContainsKey(serviceCode))
            {
                Log.Error($"{nameof(GetSmsOnlineApi)} not available for service {Enum.GetName(typeof(ServiceCode), serviceCode)}");
                return null;
            }

            var service = _services[serviceCode];
            var getNumberResult = await GetNumber(service, Enum.GetName(typeof(CountryCode), countryCode)?.ToLower());
            var getNumberResponse = getNumberResult.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (getNumberResponse.Length < 3) return null;
            //"ACCESS_NUMBER:58668155:79771317953"
            var activeSeconds = 900;
            var phoneNumberRequest = new PhoneNumberRequest { Id = getNumberResponse[1], Phone = getNumberResponse[2], Created = DateTime.UtcNow, ActiveSeconds = activeSeconds, RemainSeconds = activeSeconds };
            await SetStatus(phoneNumberRequest.Id, "1");
            return phoneNumberRequest;
        }

        public async Task<PhoneNumberValidation> GetSmsValidation(string id)
        {
            Log.Debug($"Call {nameof(GetSmsValidation)}");
            var getStatusResult = await GetStatus(id);
            var tryCount = 0;
            // if !response.IsSuccessStatusCode
            while (string.IsNullOrEmpty(getStatusResult) && tryCount < 3)
            {
                getStatusResult = await GetStatus(id);
                tryCount += 1;
            }
            if (string.IsNullOrEmpty(getStatusResult)) return null;
            var getStatusResponse = getStatusResult.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (getStatusResponse.Length == 2) { return new PhoneNumberValidation { Code = getStatusResponse[1] }; }
            tryCount = 0;
            while (getStatusResponse.Length != 2 && tryCount < 60)
            {
                Thread.Sleep(1000);
                getStatusResult = await GetStatus(id);
                getStatusResponse = getStatusResult.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (getStatusResponse.Length == 2) { return new PhoneNumberValidation { Code = getStatusResponse[1] }; }
                tryCount += 1;
            }
            return null;
        }

        public async Task SetSmsValidationSuccess(string id)
        {
            Log.Debug($"Call {nameof(SetSmsValidationSuccess)}");
            await SetStatus(id, "6");
        }

        public async Task SetNumberFail(string id)
        {
            Log.Debug($"Call {nameof(SetNumberFail)}");
            await SetStatus(id, "10");
        }

        public async Task<List<SmsServiceInfo>> GetInfo()
        {
            var list = new List<SmsServiceInfo>();
            return list;
        }

        public async Task<PhoneNumberValidation> GetSmsOnes(string id)
        {
            Log.Debug($"Call {nameof(GetSmsOnes)}");
            var getStatusResult = await GetStatus(id);
            var getStatusResponse = getStatusResult.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (getStatusResponse.Length == 2) { return new PhoneNumberValidation { Code = getStatusResponse[1] }; }
            return null;
        }
    }
}
