using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AccountData.Service;
using Common.Bot;
using Common.Service;
using GetSmsOnline;
using log4net;
using MailRu.Bot;
using OnlineSimRu;
using Phone.Service;
using Yandex.Bot;

namespace RegBot.RestApi.Controllers
{
    public class EmailController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(EmailController));
        private static readonly string AppPath = HttpRuntime.BinDirectory; //HttpRuntime.AppDomainAppPath

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
            var random = new Random();
            var values = Enum.GetValues(typeof(SmsServiceCode));
            var smsServiceCode = (SmsServiceCode)values.GetValue(random.Next(values.Length));
            values = Enum.GetValues(typeof(MailServiceCode));
            var mailServiceCode = (MailServiceCode)values.GetValue(random.Next(values.Length));
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
        [Route("newMailRuEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewMailRuEmail()
        {
            var accountData = GetRandomAccountData();
            var random = new Random();
            var values = Enum.GetValues(typeof(SmsServiceCode));
            var smsServiceCode = (SmsServiceCode)values.GetValue(random.Next(values.Length));
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

        [HttpGet]
        [Route("newYandexEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewYandexEmail()
        {
            var accountData = GetRandomAccountData();
            var random = new Random();
            var values = Enum.GetValues(typeof(SmsServiceCode));
            var smsServiceCode = (SmsServiceCode)values.GetValue(random.Next(values.Length));
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
        [Route("newMailRuEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewMailRuEmail(IAccountData accountData)
        {
            if (accountData == null) return BadRequest();
            var random = new Random();
            var values = Enum.GetValues(typeof(SmsServiceCode));
            var smsServiceCode = (SmsServiceCode)values.GetValue(random.Next(values.Length));
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
            var random = new Random();
            var values = Enum.GetValues(typeof(SmsServiceCode));
            var smsServiceCode = (SmsServiceCode)values.GetValue(random.Next(values.Length));
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

        private static async Task<IAccountData> MailRegistration(IAccountData accountData, SmsServiceCode smsServiceCode, MailServiceCode mailServiceCode)
        {
            try
            {
                ISmsService smsService;
                switch (smsServiceCode)
                {
                    case SmsServiceCode.GetSmsOnline:
                        smsService = new GetSmsOnlineApi();
                        break;
                    case SmsServiceCode.OnlineSimRu:
                        smsService = new OnlineSimRuApi();
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                accountData = await iBot.Registration(CountryCode.RU);
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
