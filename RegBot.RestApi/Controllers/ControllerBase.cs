using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;
using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using LiteDB;
using PuppeteerService;

namespace RegBot.RestApi.Controllers
{
    public class ControllerBase : ApiController
    {
        protected readonly string AppPath;// = HttpRuntime.BinDirectory; //HttpRuntime.AppDomainAppPath
        protected readonly string ConnectionString;
        protected readonly IChromiumSettings _chromiumSettings;

        public ControllerBase(IChromiumSettings chromiumSettings)
        {
            _chromiumSettings = chromiumSettings;
            AppPath = _chromiumSettings.GetPath();
            ConnectionString = Path.Combine(AppPath, ConfigurationManager.AppSettings["DbPath"]);
        }

        protected static SmsServiceCode GetRandomSmsServiceCode()
        {
            var random = new Random();
            var values = Enum.GetValues(typeof(SmsServiceCode));
            var index = random.Next(values.Length);
            var smsServiceCode = (SmsServiceCode)values.GetValue(index);
            return smsServiceCode;
        }

        protected IAccountData StoreAccountData(IAccountData accountData)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<IAccountData>("AccountsData");
                if (accountData.Id != 0)
                {
                    col.Update(accountData);
                }
                else
                {
                    var id = col.Insert(accountData).AsInt32;
                    accountData.Id = id;
                    accountData.CreatedAt = DateTime.Now;
                }
            }
            return accountData;
        }

        //private static ServiceCode GetRandomMailServiceCode()
        //{
        //    var random = new Random();
        //    var values = Enum.GetValues(typeof(ServiceCode));
        //    var index = random.Next(values.Length);
        //    var mailServiceCode = (ServiceCode)values.GetValue(index);
        //    return mailServiceCode;
        //}

        protected IAccountData GetRandomAccountData()
        {
            return new AccountDataGenerator(AppPath).GetRandom();
        }
    }
}