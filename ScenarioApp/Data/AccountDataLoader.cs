using System.Collections.Generic;
using System.Linq;
using Common.Service;
using Common.Service.Enums;
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
        private IAccountData mailruAccount;
        private IAccountData yandexAccount;
        private IAccountData gmailAccount;
        private IAccountData okAccount;

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

        public IAccountData MailruAccount {
            get
            {
                if (mailruAccount == null) mailruAccount = GetMailruAccountData()?.FirstOrDefault();
                return mailruAccount;
            }
            set => mailruAccount = value;
        }

        public IAccountData YandexAccount
        {
            get
            {
                if (yandexAccount == null) yandexAccount = GetYandexAccountData()?.FirstOrDefault();
                return yandexAccount;
            }
            set => yandexAccount = value;
        }

        public IAccountData GmailAccount
        {
            get
            {
                if (gmailAccount == null) gmailAccount = GetGmailAccountData()?.FirstOrDefault();
                return gmailAccount;
            }
            set => gmailAccount = value;
        }

        public IAccountData OkAccount
        {
            get
            {
                if (okAccount == null) okAccount = GetOkAccountData()?.FirstOrDefault();
                return okAccount;
            }
            set => okAccount = value;
        }

        public IList<IAccountData> GetFbAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                var data = db.GetCollection<IAccountData>(CollectionName)
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), ServiceDomains.GetDomain(ServiceCode.Facebook)),
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
                //        Query.EQ(nameof(IAccountData.Domain), ServiceDomains.GetDomain(ServiceCode.Vk)),
                //        Query.EQ(nameof(IAccountData.Success), true)), Query.EQ(nameof(IAccountData.Sex), "Male")))
                //;
                var data = db.GetCollection<IAccountData>(CollectionName)
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), ServiceDomains.GetDomain(ServiceCode.Vk)),
                    Query.EQ(nameof(IAccountData.Success), true)
                    ))
                ;
                var list = new List<IAccountData>();
                list.AddRange(data);
                return list;
            }
        }

        public IList<IAccountData> GetMailruAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                var data = db.GetCollection<IAccountData>(CollectionName)
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), ServiceDomains.GetDomain(ServiceCode.MailRu)),
                    Query.EQ(nameof(IAccountData.Success), true)
                    ))
                ;
                var list = new List<IAccountData>();
                list.AddRange(data);
                return list;
            }
        }

        public IList<IAccountData> GetYandexAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                var data = db.GetCollection<IAccountData>(CollectionName)
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), ServiceDomains.GetDomain(ServiceCode.Yandex)),
                    Query.EQ(nameof(IAccountData.Success), true)
                    ))
                ;
                var list = new List<IAccountData>();
                list.AddRange(data);
                return list;
            }
        }

        public IList<IAccountData> GetGmailAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                var data = db.GetCollection<IAccountData>(CollectionName)
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), ServiceDomains.GetDomain(ServiceCode.Gmail)),
                    Query.EQ(nameof(IAccountData.Success), true)
                    ))
                ;
                var list = new List<IAccountData>();
                list.AddRange(data);
                return list;
            }
        }

        public IList<IAccountData> GetOkAccountData()
        {
            using (var db = new LiteDatabase(_dataSettings.GetConnectionString()))
            {
                var data = db.GetCollection<IAccountData>(CollectionName)
                    .Find(Query.And(Query.EQ(nameof(IAccountData.Domain), ServiceDomains.GetDomain(ServiceCode.Ok)),
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
