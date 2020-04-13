using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SimSmsOrg
{
    public class SimSmsOrgApi : ISmsService
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(SimSmsOrgApi));

        private readonly string _apiKeySimSmsOrg = System.Configuration.ConfigurationManager.AppSettings[nameof(_apiKeySimSmsOrg)];

        private readonly HttpClient _apiHttpClient;
        private readonly string _endpointGetBalance;
        //private readonly string _endpointGetNumbersStatus;
        private readonly string _endpointGetNumber;
        private readonly string _endpointSetStatus;
        private readonly string _endpointGetStatus;

        private readonly string _endpointGetNumberCount;
        private readonly string _endpointGetNumberPrice;

        private readonly Dictionary<ServiceCode, string> _services = new Dictionary<ServiceCode, string>();
        private readonly Dictionary<ServiceCode, string> _servicesAlt = new Dictionary<ServiceCode, string>();
        private static readonly Dictionary<CountryCode, string> CountryParams = new Dictionary<CountryCode, string>();
        private readonly Dictionary<CountryCode, string> CountryParamsAlt = new Dictionary<CountryCode, string>();

        #endregion

        public SimSmsOrgApi()
        {
            var baseUrl = $"http://simsms.org/stubs/handler_api.php?api_key={_apiKeySimSmsOrg}";
            _apiHttpClient = new HttpClient(new LoggingHandler());
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _endpointGetBalance = $"{baseUrl}&action=getBalance";
            //_endpointGetNumbersStatus = $"{BaseUrl}&action=getNumbersStatus";
            _endpointGetNumber = $"{baseUrl}&action=getNumber";
            _endpointSetStatus = $"{baseUrl}&action=setStatus";
            _endpointGetStatus = $"{baseUrl}&action=getStatus";

            var baseUrl2 = $"http://simsms.org/priemnik.php?apikey={_apiKeySimSmsOrg}";
            _endpointGetNumberCount = $"{baseUrl2}&metod=get_count_new";
            _endpointGetNumberPrice = $"{baseUrl2}&metod=get_service_price";

            _services[ServiceCode.MailRu] = "mg";
            //_services[ServiceCode.MailRu] = "ma";
            _services[ServiceCode.Yandex] = "ya";
            _services[ServiceCode.Gmail] = "go";
            _services[ServiceCode.Other] = "ot";
            _services[ServiceCode.Facebook] = "fb";
            _services[ServiceCode.Vk] = "mg";
            //_services[ServiceCode.Vk] = "vk";
            _services[ServiceCode.Ok] = "mg";
            //_services[ServiceCode.Ok] = "ok";



            CountryParams[CountryCode.RU] = "0";
            CountryParams[CountryCode.UA] = "1";
            CountryParams[CountryCode.KZ] = "2";
            CountryParams[CountryCode.CN] = "3";
            CountryParams[CountryCode.GE] = "5";
            CountryParams[CountryCode.KG] = "11";
            CountryParams[CountryCode.PL] = "15";
            CountryParams[CountryCode.EN] = "16";
            CountryParams[CountryCode.EG] = "21";
            CountryParams[CountryCode.DE] = "43";
            CountryParams[CountryCode.LV] = "49";
            CountryParams[CountryCode.UZ] = "40";
            CountryParams[CountryCode.AT] = "50";
            CountryParams[CountryCode.FR] = "22";
            CountryParams[CountryCode.CM] = "41";
            CountryParams[CountryCode.TD] = "42";
            CountryParams[CountryCode.NL] = "48";
            CountryParams[CountryCode.NG] = "19";
            CountryParams[CountryCode.HT] = "26";
            CountryParams[CountryCode.RS] = "29";
            CountryParams[CountryCode.YE] = "30";
            CountryParams[CountryCode.CI] = "27";
            /*
             4 - Филиппины, 5 - Грузия, 
            6 - Индонезия, 7 - Белорусь, 8 - Кения, 10 - Бразилия, 
            12 - США, 13 - Израиль, 14 - Парагвай,   
            17 - США (Virtual), 18 - Финляндия, 20 - Макао
            23 - Ирландия, 24 - Камбоджа, 25 - Лаос, 
            28 - Гамбия,  31 - ЮАР, 
            32 - Румыния, 33 - Швеция, 34 - Эстония, 35 - Азербайджан, 36 - Канада, 
            37 - Марокко, 38 - Гана, 39 - Аргентина, 
            44 - Литва, 45 - Хорватия, 47 - Ирак, 
            51 - Беларусь, 52 - Таиланд, 
            53 - Сауд. Аравия, 54 - Мексика, 55 - Тайвань, 56 - Испания, 57 - Иран, 
            58 - Алжир, 59 - Словения, 60 - Бангладеш, 61 - Сенегал, 62 - Турция, 
            63 - Чехия, 64 - Шри-Ланка, 65 - Перу, 66 - Пакистан, 67 - Новая Зеландия, 
            68 - Гвинея, 69 - Мали, 70 - Венесуэла, 71 - Эфиопия
             */

            _servicesAlt[ServiceCode.MailRu] = "opt4";
            _servicesAlt[ServiceCode.Yandex] = "opt23";
            _servicesAlt[ServiceCode.Gmail] = "opt1";
            _servicesAlt[ServiceCode.Other] = "opt19";
            _servicesAlt[ServiceCode.Facebook] = "opt2";
            _servicesAlt[ServiceCode.Vk] = "opt4";
            _servicesAlt[ServiceCode.Ok] = "opt4";

            foreach (CountryCode countryCode in Enum.GetValues(typeof(CountryCode)))
            {
                CountryParamsAlt[countryCode] = Enum.GetName(countryCode.GetType(), countryCode);
            }


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

        private async Task<string> GetBalanceCall()
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetBalance}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetBalanceCall)}... {result}");
                return result.Substring(15);
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

        private async Task<int> GetNumberCount(string country, string service)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetNumberCount}&country={country}&service={service}"))
            {
                if (!response.IsSuccessStatusCode) return 0;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetNumberCount)}... {result}");
                if (result.Contains(nameof(SimSmsNumberCount.online)))
                {
                    var simSmsNumberCount = JsonConvert.DeserializeObject<SimSmsNumberCount>(result);
                    if (simSmsNumberCount != null && int.TryParse(simSmsNumberCount.online, out int count)) return count;
                }
                return 0;
            }
        }

        private async Task<double> GetNumberPrice(string country, string service)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetNumberPrice}&country={country}&service={service}"))
            {
                if (!response.IsSuccessStatusCode) return 0;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetNumberPrice)}... {result}");
                if (result.Contains(nameof(SimSmsNumberPrice.price)))
                {
                    var simSmsNumberPrice = JsonConvert.DeserializeObject<SimSmsNumberPrice>(result);
                    if (simSmsNumberPrice != null && double.TryParse(simSmsNumberPrice.price, NumberStyles.Any, CultureInfo.InvariantCulture, out double price)) return price;
                }
                return 0;
            }
        }

        public async Task<PhoneNumberRequest> GetPhoneNumber(CountryCode countryCode, ServiceCode serviceCode)
        {
            Log.Debug($"Call {nameof(GetPhoneNumber)}");
            //if (!_countries.ContainsKey(countryCode))
            //{
            //    Log.Error($"{nameof(SimSmsOrgApi)} not available for country {Enum.GetName(typeof(CountryCode), countryCode)}");
            //    return null;
            //}
            if (!_services.ContainsKey(serviceCode))
            {
                Log.Error($"{nameof(SimSmsOrgApi)} not available for service {Enum.GetName(typeof(ServiceCode), serviceCode)}");
                return null;
            }
            if (!CountryParams.ContainsKey(countryCode))
            {
                var random = new Random();
                var values = CountryParams.Keys.ToList();
                var size = CountryParams.Count;
                countryCode = values[random.Next(size)];
            }
            var getNumberResult = await GetNumber(_services[serviceCode], CountryParams[countryCode]);
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
            await SetStatus(id, "8");
        }

        public async Task<List<SmsServiceInfo>> GetInfo()
        {
            var list = new List<SmsServiceInfo>();
            foreach (var pairServicesAlt in _servicesAlt)
            {
                foreach (var pairCountryParamsAlt in CountryParamsAlt)
                {
                    var count = await GetNumberCount(country: pairCountryParamsAlt.Value, service: pairServicesAlt.Value);
                    if (count > 0)
                    {
                        var price = await GetNumberPrice(country: pairCountryParamsAlt.Value, service: pairServicesAlt.Value);
                        if (price <= 0) continue;
                        list.Add(new SmsServiceInfo
                        {
                            SmsServiceCode = SmsServiceCode.SimSmsOrg,
                            CountryCode = pairCountryParamsAlt.Key,
                            ServiceCode = pairServicesAlt.Key,
                            NumberCount = count,
                            Price = price
                        });
                    }
                }
            }
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

        public async Task<double> GetBalance()
        {
            var balance = await GetBalanceCall();
            if (double.TryParse(balance, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out double result)) return result;
            return 0;
        }
    }
}
