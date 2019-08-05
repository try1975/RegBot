using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using GetSmsOnline;
using Gmail.Bot;
using LiteDB;
using log4net;
using MailRu.Bot;
using NickBuhro.Translit;
using OnlineSimRu;
using SimSmsOrg;
using Yandex.Bot;

namespace RegBot.RestApi.Controllers
{
    public class EmailController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(EmailController));
        private static readonly string AppPath = HttpRuntime.BinDirectory; //HttpRuntime.AppDomainAppPath
        private readonly string connectionString;

        public EmailController()
        {
            connectionString = Path.Combine(AppPath, ConfigurationManager.AppSettings["DbPath"]);
        }

        private IAccountData StoreAccountData(IAccountData accountData)
        {
            using (var db = new LiteDatabase(connectionString))
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

        [HttpGet]
        [Route(nameof(InitialAccountData))]
        [ResponseType(typeof(IAccountData))]
        public IHttpActionResult InitialAccountData()
        {
            return Ok(GetRandomAccountData());
        }

        [HttpGet]
        [Route("newEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewEmail()
        {
            var accountData = GetRandomAccountData();
            var smsServiceCode = GetRandomSmsServiceCode();
            var mailServiceCode = GetRandomMailServiceCode();
            try
            {
                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, mailServiceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        private static MailServiceCode GetRandomMailServiceCode()
        {
            var random = new Random();
            var values = Enum.GetValues(typeof(MailServiceCode));
            var index = random.Next(values.Length);
            var mailServiceCode = (MailServiceCode)values.GetValue(index);
            return mailServiceCode;
        }

        private static SmsServiceCode GetRandomSmsServiceCode()
        {
            var random = new Random();
            var values = Enum.GetValues(typeof(SmsServiceCode));
            var index = random.Next(values.Length);
            var smsServiceCode = (SmsServiceCode)values.GetValue(index);
            return smsServiceCode;
        }

        [HttpGet]
        [Route("newMailRuEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewMailRuEmail()
        {
            IAccountData accountData;
            try
            {
                accountData = GetRandomAccountData();
                var smsServiceCode = GetRandomSmsServiceCode();
                const MailServiceCode mailServiceCode = MailServiceCode.MailRu;

                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, mailServiceCode);
                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(accountData);
        }

        [HttpGet]
        [Route("newYandexEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewYandexEmail()
        {
            var accountData = GetRandomAccountData();
            var smsServiceCode = GetRandomSmsServiceCode();
            const MailServiceCode mailServiceCode = MailServiceCode.Yandex;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, mailServiceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpGet]
        [Route("newGmailEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewGmailEmail()
        {
            var accountData = GetRandomAccountData();
            var smsServiceCode = GetRandomSmsServiceCode();
            const MailServiceCode mailServiceCode = MailServiceCode.Gmail;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, mailServiceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpPost]
        [Route("newMailRuEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewMailRuEmail(IAccountData accountData)
        {
            if (accountData == null) return BadRequest();
            var smsServiceCode = GetRandomSmsServiceCode();
            const MailServiceCode mailServiceCode = MailServiceCode.MailRu;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, mailServiceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpPost]
        [Route("newYandexEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewYandexEmail(IAccountData accountData)
        {
            if (accountData == null) return BadRequest();
            var smsServiceCode = GetRandomSmsServiceCode();
            const MailServiceCode mailServiceCode = MailServiceCode.Yandex;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, mailServiceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpPost]
        [Route("newGmailEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewGmailEmail(IAccountData accountData)
        {
            if (accountData == null) return BadRequest();
            var smsServiceCode = GetRandomSmsServiceCode();
            const MailServiceCode mailServiceCode = MailServiceCode.Gmail;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, mailServiceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        private async Task<IAccountData> MailRegistration(IAccountData accountData, SmsServiceCode smsServiceCode, MailServiceCode mailServiceCode)
        {
            try
            {
                if (string.IsNullOrEmpty(accountData.AccountName))
                {
                    accountData.AccountName = Transliteration.CyrillicToLatin($"{accountData.Firstname.ToLower()}.{accountData.Lastname.ToLower()}", Language.Russian);
                }
                accountData = StoreAccountData(accountData);
                ISmsService smsService;
                switch (smsServiceCode)
                {
                    case SmsServiceCode.GetSmsOnline:
                        smsService = new GetSmsOnlineApi();
                        break;
                    case SmsServiceCode.OnlineSimRu:
                        smsService = new OnlineSimRuApi();
                        break;
                    case SmsServiceCode.SimSmsOrg:
                        smsService = new SimSmsOrgApi();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                IBot iBot;
                switch (mailServiceCode)
                {
                    case MailServiceCode.MailRu:
                        iBot = new MailRuRegistration(accountData, smsService, AppPath);
                        break;
                    case MailServiceCode.Yandex:
                        iBot = new YandexRegistration(accountData, smsService, AppPath);
                        break;
                    case MailServiceCode.Gmail:
                        iBot = new GmailRegistration(accountData, smsService, AppPath);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                accountData = await iBot.Registration(CountryCode.RU, headless: true);
                StoreAccountData(accountData);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            return accountData;
        }

        private static IAccountData GetRandomAccountData()
        {
            return new AccountDataGenerator(AppPath).GetRandom();
        }
    }
}
