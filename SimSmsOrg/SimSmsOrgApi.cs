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

namespace SimSmsOrg
{
    public class SimSmsOrgApi : ISmsService
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(SimSmsOrgApi));

        private readonly string _apiKeySimSmsOrg = System.Configuration.ConfigurationManager.AppSettings[nameof(_apiKeySimSmsOrg)];

        private readonly HttpClient _apiHttpClient;
        //private readonly string _endpointGetBalance;
        //private readonly string _endpointGetNumbersStatus;
        private readonly string _endpointGetNumber;
        private readonly string _endpointSetStatus;
        private readonly string _endpointGetStatus;

        private readonly Dictionary<MailServiceCode, string> _mailServices = new Dictionary<MailServiceCode, string>();
        private static readonly Dictionary<CountryCode, string> CountryParams = new Dictionary<CountryCode,string>();
        
        #endregion

        public SimSmsOrgApi()
        {
            var baseUrl = $"http://simsms.org/stubs/handler_api.php?api_key={_apiKeySimSmsOrg}";
            _apiHttpClient = new HttpClient(new LoggingHandler());
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_endpointGetBalance = $"{BaseUrl}&action=getBalance";
            //_endpointGetNumbersStatus = $"{BaseUrl}&action=getNumbersStatus";
            _endpointGetNumber = $"{baseUrl}&action=getNumber";
            _endpointSetStatus = $"{baseUrl}&action=setStatus";
            _endpointGetStatus = $"{baseUrl}&action=getStatus";

            _mailServices[MailServiceCode.MailRu] = "ma";
            _mailServices[MailServiceCode.Yandex] = "ya";
            _mailServices[MailServiceCode.Gmail] = "go";
            _mailServices[MailServiceCode.Other] = "ot";

            CountryParams[CountryCode.RU] = "";
            CountryParams[CountryCode.UA] = "1";
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

        public async Task<PhoneNumberRequest> GetPhoneNumber(CountryCode countryCode, MailServiceCode mailServiceCode)
        {
            Log.Debug($"Call {nameof(GetPhoneNumber)}");
            var getNumberResult = await GetNumber(_mailServices[mailServiceCode], CountryParams[countryCode]);
            var getNumberResponse = getNumberResult.Split(new []{':'}, StringSplitOptions.RemoveEmptyEntries);
            if (getNumberResponse.Length < 3) return null;
            //"ACCESS_NUMBER:58668155:79771317953"
            var phoneNumberRequest= new PhoneNumberRequest
            {
                Id = getNumberResponse[1],
                Phone = getNumberResponse[2]
            };
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
            var getStatusResponse = getStatusResult.Split(new []{':'}, StringSplitOptions.RemoveEmptyEntries);
            if (getStatusResponse.Length == 2) { return new PhoneNumberValidation{Code = getStatusResponse[1]}; }
            tryCount = 0;
            while (getStatusResponse.Length != 2 && tryCount < 30)
            {
                Thread.Sleep(1000);
                getStatusResult = await GetStatus(id);
                getStatusResponse = getStatusResult.Split(new []{':'}, StringSplitOptions.RemoveEmptyEntries);
                if (getStatusResponse.Length == 2) { return new PhoneNumberValidation{Code = getStatusResponse[1]}; }
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
            await SetStatus(id, "8");
        }
    }
}
