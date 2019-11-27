using System.Collections.Generic;
using Common.Service.Interfaces;
using LiteDB;

namespace ScenarioApp.Data
{
    public class AccountDataLoader : IAccountDataLoader
    {
        private readonly IDataSettings _dataSettings;
        public AccountDataLoader(IDataSettings dataSettings)
        {
            _dataSettings = dataSettings;
        }

        public IList<IAccountData> GetAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                var data = db.GetCollection<IAccountData>("AccountsData")
                    .Find(Query.And(Query.And(
                        Query.EQ(nameof(IAccountData.Domain), "vk.com"),
                        Query.EQ(nameof(IAccountData.Success), true)), Query.EQ(nameof(IAccountData.Sex), "Male")))
                ;
                //var data = db.GetCollection<IAccountData>("AccountsData")
                //    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), "vk.com"),
                //    Query.EQ(nameof(IAccountData.Success), true)
                //    ))
                //;
                var list = new List<IAccountData>();
                list.AddRange(data);
                return list;
            }
        }
    }
}
