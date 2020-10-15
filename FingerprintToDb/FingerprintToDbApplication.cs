using Fingerprint.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FingerprintToDb
{
    public class FingerprintToDbApplication
    {
        private readonly ILogger<FingerprintToDbApplication> _logger;
        private readonly List<string> _fingerprintsFolders;
        private readonly FingerprintStore _fingerprintStore;
        private const string _fingerprintsFoldersKey = "FingerprintsFolders";

        public FingerprintToDbApplication(ILogger<FingerprintToDbApplication> logger, IConfiguration configuration, FingerprintStore fingerprintStore)
        {
            _logger = logger;
            _fingerprintsFolders = configuration.GetSection(_fingerprintsFoldersKey).Get<List<string>>();
            _fingerprintStore = fingerprintStore;
        }
        internal async Task<string> Run()
        {
            //var results = _fingerprintStore.Find(x => x.Age > 20);
            foreach (var fingerprintFolder in _fingerprintsFolders)
            {
                // foreach fingerprint file
                var files = Directory.GetFiles(fingerprintFolder);
                foreach (var path in files)
                {
                    try
                    {
                        //if (!path.Contains("qkna41")) continue;
                        var strFingerprint = File.ReadAllText(path);
                        //replace "constraints": []
                        strFingerprint = strFingerprint.Replace("\"constraints\": []", "\"constraints\": {}");
                        strFingerprint = strFingerprint.Replace("\"constraints\":[]", "\"constraints\": {}");
                        var fingerprint = JsonConvert.DeserializeObject<Fingerprint.Classes.Fingerprint>(strFingerprint, new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        if (!fingerprint.valid.HasValue || !fingerprint.valid.Value) continue;
                        _fingerprintStore.StoreData(fingerprint);
                    }
                    catch (System.Exception exception)
                    {
                        _logger.LogError(exception.Message);
                    }
                }
            }

            return "";
        }
    }
}
