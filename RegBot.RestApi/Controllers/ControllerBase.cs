using AccountData.Service;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using LiteDB;
using log4net;
using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using NickBuhro.Translit;
using MailRu.Bot;
using Yandex.Bot;
using Gmail.Bot;
using Facebook.Bot;
using Vk.Bot;
using Ok.Bot;
using System.Linq;

namespace RegBot.RestApi.Controllers
{
    public class ControllerBase : ApiController
    {
        #region protected
        protected readonly string AppPath;// = HttpRuntime.BinDirectory; //HttpRuntime.AppDomainAppPath
        protected readonly string ConnectionString;
        protected readonly IChromiumSettings _chromiumSettings;
        protected readonly ISmsServices _smsServices; 
        #endregion

        public ControllerBase(IChromiumSettings chromiumSettings, ISmsServices smsServices = null)
        {
            _chromiumSettings = chromiumSettings;
            _smsServices = smsServices;
            AppPath = _chromiumSettings.GetPath();
            ConnectionString = Path.Combine(AppPath, ConfigurationManager.AppSettings["DbPath"]);
        }

        protected async Task<IAccountData> TryRegister(IEnumerable<SmsServiceInfo> infos)
        {
            var accountData = GetRandomAccountData();
            foreach (var info in infos)
            {
                accountData = GetRandomAccountData(info.CountryCode);
                accountData.PhoneCountryCode = Enum.GetName(typeof(CountryCode), info.CountryCode);
                accountData = await Registration(accountData, info.SmsServiceCode, info.ServiceCode);
                if (accountData == null) break;
                if (accountData.Success) break;
                if (string.IsNullOrEmpty(accountData.ErrMsg)) break;
                if (accountData.ErrMsg.Equals(BotMessages.NoPhoneNumberMessage)) info.Skiped = true;
                if (!BotMessages.BadNumber.Contains(accountData.ErrMsg)) break;
                await _smsServices.AddFail(info);
            }
            return accountData;
        }

        protected async Task<IAccountData> Registration(IAccountData accountData, SmsServiceCode smsServiceCode, ServiceCode serviceCode, CountryCode countryCode = CountryCode.RU, ILog log=null)
        {
            try
            {
                log?.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                if (string.IsNullOrEmpty(accountData.AccountName))
                {
                    accountData.AccountName = Transliteration.CyrillicToLatin($"{accountData.Firstname.ToLower()}.{accountData.Lastname.ToLower()}", Language.Russian);
                    accountData.AccountName = accountData.AccountName.Replace("`", "");
                }
                accountData = StoreAccountData(accountData);
                ISmsService smsService = _smsServices.GetSmsService(smsServiceCode);
                IBot iBot;
                switch (serviceCode)
                {
                    case ServiceCode.MailRu:
                        iBot = new MailRuRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    case ServiceCode.Yandex:
                        iBot = new YandexRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    case ServiceCode.Gmail:
                        iBot = new GmailRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    case ServiceCode.Facebook:
                        iBot = new FacebookRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    case ServiceCode.Vk:
                        iBot = new VkRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    case ServiceCode.Ok:
                        iBot = new OkRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                //var countryCode = CountryCode.RU;
                if (!string.IsNullOrEmpty(accountData.PhoneCountryCode))
                {
                    countryCode = (CountryCode)Enum.Parse(typeof(CountryCode), accountData.PhoneCountryCode);
                }
                accountData = await iBot.Registration(countryCode);
                StoreAccountData(accountData);
            }
            catch (Exception exception)
            {
                log?.Error(exception);
            }
            finally
            {
                log?.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            }
            return accountData;
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

        protected IAccountData GetRandomAccountData(CountryCode countryCode = CountryCode.RU)
        {
            return new AccountDataGenerator(AppPath).GetRandom(countryCode);
        }
    }
}