using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fingerprint.Classes
{
    public class FingerprintStore
    {
        private readonly ILogger<FingerprintStore> _logger;
        //private readonly Logger<FingerprintStore> _liteLogger;
        private readonly string _connectionString;
        private readonly string _collectionName;
        public FingerprintStore(ILogger<FingerprintStore> logger, IConfiguration configuration)
        {
            _logger = logger;
            //_liteLogger = new Logger(level: Logger.QUERY, _liteLogger_Logging);
            _connectionString = configuration["connectionString"];
            _collectionName = nameof(Fingerprint);
        }

        private void _liteLogger_Logging(string message)
        {
            _logger.LogInformation(message);
        }

        public Fingerprint StoreData(Fingerprint fingerprint)
        {
            using (var db = new LiteDatabase(_connectionString))//, mapper:null, _liteLogger))
            {
                var col = db.GetCollection<Fingerprint>(_collectionName);
                if (fingerprint.Id != 0)
                {
                    col.Update(fingerprint);
                }
                else
                {
                    var id = col.Insert(fingerprint).AsInt32;
                    fingerprint.Id = id;
                    //accountData.CreatedAt = DateTime.Now;
                }
            }
            return fingerprint;
        }
    }
}
