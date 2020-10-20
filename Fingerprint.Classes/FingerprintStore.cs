using LiteDB;
using System;

namespace Fingerprint.Classes
{
    public class FingerprintStore : IFingerprintStore
    {
       // private readonly ILogger<FingerprintStore> _logger;
        private readonly string _connectionString;
        private readonly string _collectionName = nameof(Fingerprint);
        private readonly Random  rnd = new Random();

        //public FingerprintStore(ILogger<FingerprintStore> logger, IConfiguration configuration)
        //{
        //    _logger = logger;
        //    _connectionString = configuration["connectionString"];
        //}

        public FingerprintStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Fingerprint GetRandom()
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Fingerprint>(_collectionName);
                var offset = rnd.Next(0, col.Count());
                return col.Query().Limit(1).Offset(offset).SingleOrDefault();
            }
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
