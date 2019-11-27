using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using GetSmsOnline;
using Gmail.Bot;
using log4net;
using MailRu.Bot;
using NickBuhro.Translit;
using OnlineSimRu;
using PuppeteerService;
using SimSmsOrg;
using Yandex.Bot;

namespace RegBot.RestApi.Controllers
{
    public class EmailController : ControllerBase
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(EmailController));

        public EmailController(IChromiumSettings chromiumSettings) : base(chromiumSettings)
        {
        }

        [HttpGet]
        [Route(nameof(InitialAccountData))]
        [ResponseType(typeof(IAccountData))]
        public IHttpActionResult InitialAccountData()
        {
            return Ok(GetRandomAccountData());
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
                const ServiceCode serviceCode = ServiceCode.MailRu;

                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, serviceCode);
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
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
            const ServiceCode serviceCode = ServiceCode.Yandex;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, serviceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpGet]
        [Route("newGmailEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewGmailEmail()
        {
            var accountData = GetRandomAccountData();
            var smsServiceCode = GetRandomSmsServiceCode();
            const ServiceCode serviceCode = ServiceCode.Gmail;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, serviceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpPost]
        [Route("newMailRuEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewMailRuEmail(EmailAccountData data)
        {
            if (data == null) return BadRequest();
            var accountData = (IAccountData)data;
            var smsServiceCode = GetRandomSmsServiceCode();
            const ServiceCode serviceCode = ServiceCode.MailRu;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, serviceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpPost]
        [Route("newYandexEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewYandexEmail(EmailAccountData data)
        {
            if (data == null) return BadRequest();
            var accountData = (IAccountData)data;
            var smsServiceCode = GetRandomSmsServiceCode();
            const ServiceCode serviceCode = ServiceCode.Yandex;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, serviceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        [HttpPost]
        [Route("newGmailEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewGmailEmail(EmailAccountData data)
        {
            if (data == null) return BadRequest();
            var accountData = (IAccountData)data;
            var smsServiceCode = GetRandomSmsServiceCode();
            const ServiceCode serviceCode = ServiceCode.Gmail;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, smsServiceCode, serviceCode);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            return Ok(accountData);
        }

        private async Task<IAccountData> MailRegistration(IAccountData accountData, SmsServiceCode smsServiceCode, ServiceCode serviceCode)
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
                switch (serviceCode)
                {
                    case ServiceCode.MailRu:
                        iBot = new MailRuRegistration(accountData, smsService, AppPath);
                        break;
                    case ServiceCode.Yandex:
                        iBot = new YandexRegistration(accountData, smsService, AppPath);
                        break;
                    case ServiceCode.Gmail:
                        iBot = new GmailRegistration(accountData, smsService, AppPath);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var countryCode = CountryCode.RU;
                if (!string.IsNullOrEmpty(accountData.PhoneCountryCode))
                {
                    countryCode = (CountryCode)Enum.Parse(typeof(CountryCode), accountData.PhoneCountryCode);
                }
               
                accountData = await iBot.Registration(countryCode, headless: true);
                StoreAccountData(accountData);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            return accountData;
        }

        
    }
}
