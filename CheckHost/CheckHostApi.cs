using Common.Service;
using IpCommon;
using log4net;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckHost
{
    //https://check-host.net/ip-info?host=5.39.200.88:54987
    public class CheckHostApi : IIpInfoService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CheckHostApi));
        private readonly HttpClient _apiHttpClient;
        private readonly string _endpointGetInfo;

        private Regex _exCountryCode = new Regex("strong>\\s*\\(([A-Z]{2})\\)\\s*<");
        private Regex _exTimeZone = new Regex("<td>\\s*(\\S*), GMT\\S*\\s*<\\/td>");

        public CheckHostApi()
        {
            _apiHttpClient = new HttpClient(new LoggingHandler());
            var baseUrl = "https://check-host.net/ip-info?host=";
            _endpointGetInfo = baseUrl;
        }

        private async Task<IpInfo> GetCheckHostInfo(string host)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetInfo}{host}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsStringAsync();
                //Log.Debug($"{nameof(GetCheckHostInfo)}... {result}");
                var matchCountryCode = _exCountryCode.Match(result);
                var countryCode = matchCountryCode.Groups[1].Value;
                var matchTimeZone = _exTimeZone.Match(result);
                var timeZone = matchTimeZone.Groups[1].Value;
                return new IpInfo { CountryCode = countryCode, TimeZone = timeZone };
            }
        }

        public async Task<IpInfo> GetIpInfo(string ip)
        {
            return await GetCheckHostInfo(ip);
            /*
                public string CountryCode { get; set; }
                public string LanguageCode { get; set; }
                public string TimeZone { get; set; }
             */
        }
    }
}
