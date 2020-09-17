using Common.Service;
using IpCommon;
using log4net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ip2location
{
    public class Ip2LocationApi : IIpInfoService
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(Ip2LocationApi));
        private readonly string _apiKeyIp2Location = System.Configuration.ConfigurationManager.AppSettings[nameof(_apiKeyIp2Location)];

        private readonly HttpClient _apiHttpClient;
        private readonly string _endpointGetInfo;
        #endregion

        public Ip2LocationApi()
        {
            _apiHttpClient = new HttpClient(new LoggingHandler());
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //https://api.ip2location.com/v2/?ip=88.150.236.115&key=demo&package=WS11&addon=country,time_zone_info&lang=ru
            if (string.IsNullOrEmpty(_apiKeyIp2Location)) _apiKeyIp2Location = "demo"; //2G8NSHAJW3
            var baseUrl = $"https://api.ip2location.com/v2/?key={_apiKeyIp2Location}&package=WS11&addon=country,time_zone_info&format=json";
            _endpointGetInfo = baseUrl;
        }

        private async Task<Ip2LocationInfo> GetIp2LocationInfo(string ip)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetInfo}&ip={ip}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                Log.Debug($"{nameof(GetIp2LocationInfo)}... {result}");
                if (result.Contains("Invalid IP address")) return null;
                return JsonConvert.DeserializeObject<Ip2LocationInfo>(result);
            }
        }

        async Task<IpInfo> IIpInfoService.GetIpInfo(string ip)
        {
            var ip2LocationInfo = await GetIp2LocationInfo(ip);
            if (ip2LocationInfo == null) return null;
            var ipInfo = new IpInfo
            {
                CountryCode = ip2LocationInfo.CountryCode,
                LanguageCode = ip2LocationInfo.country.Language.Code,
                TimeZone = ip2LocationInfo.time_zone_info.Olson
            };
            return ipInfo;
        }
    }
}
