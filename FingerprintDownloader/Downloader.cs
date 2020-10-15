using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FingerprintDownloader
{
    public class Downloader
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly string _baseURL;
        private readonly string _fingerprintFolder;

        public Downloader(ILogger<Downloader> logger, IHttpClientFactory httpFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpFactory = httpFactory;
            _baseURL = configuration["ServerURL"];
            _fingerprintFolder = Path.GetFullPath(configuration["FingerprintFolder"]);
            Directory.CreateDirectory(_fingerprintFolder);
        }

        internal async Task<string> Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            var request = new HttpRequestMessage(HttpMethod.Get, _baseURL);
            var client = _httpFactory.CreateClient();
            var response = await client.SendAsync(request);

            var result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                var path = Path.Combine(_fingerprintFolder, Path.GetRandomFileName());
                var strFingerprint = await response.Content.ReadAsStringAsync();
                var fingerprint = JsonConvert.DeserializeObject<Fingerprint.Classes.Fingerprint>(strFingerprint, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                if (fingerprint.valid.HasValue && fingerprint.valid.Value)
                {
                    File.WriteAllText(path, strFingerprint);
                    result = path;
                }
                else result = "fingerprint is not valid";
            }
            else
            {
                result = $"StatusCode: {response.StatusCode}";
            }
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);
            return result;
        }
    }
}
