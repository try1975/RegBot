using System.Collections.Generic;
using System.Linq;
using Common.Service.Interfaces;
using LiteDB;

namespace ScenarioApp.Data
{
    public class AccountDataLoader : IAccountDataLoader
    {
        private readonly IDataSettings _dataSettings;
        private static readonly string CollectionName = "AccountsData";
        private IAccountData vkAccount;
        private IAccountData fbAccount;

        public AccountDataLoader(IDataSettings dataSettings)
        {
            _dataSettings = dataSettings;
        }

        public IAccountData VkAccount
        {
            get
            {
                if (vkAccount == null) vkAccount = GetVkAccountData()?.FirstOrDefault();
                return vkAccount;
            }
            set => vkAccount = value;
        }
        public IAccountData FbAccount
        {
            get
            {
                if (fbAccount == null) fbAccount = GetFbAccountData()?.FirstOrDefault();
                return fbAccount;
            }
            set => fbAccount = value;
        }

        public IList<IAccountData> GetFbAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                var data = db.GetCollection<IAccountData>(CollectionName)
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), "facebook.com"),
                    Query.EQ(nameof(IAccountData.Success), true)
                    ))
                ;
                var list = new List<IAccountData>();
                list.AddRange(data);
                return list;
            }
        }

        public IList<IAccountData> GetVkAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                //var data = db.GetCollection<IAccountData>(CollectionName)
                //    .Find(Query.And(Query.And(
                //        Query.EQ(nameof(IAccountData.Domain), "vk.com"),
                //        Query.EQ(nameof(IAccountData.Success), true)), Query.EQ(nameof(IAccountData.Sex), "Male")))
                //;
                var data = db.GetCollection<IAccountData>("AccountsData")
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), "vk.com"),
                    Query.EQ(nameof(IAccountData.Success), true)
                    ))
                ;
                var list = new List<IAccountData>();
                list.AddRange(data);
                return list;
            }
        }
    }
}
