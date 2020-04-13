using AccountData.Service;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Gmail.Bot;
using log4net;
using MailRu.Bot;
using NickBuhro.Translit;
using PuppeteerService;
using ScenarioService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Yandex.Bot;

namespace RegBot.RestApi.Controllers
{
    public class EmailController : ControllerBase
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(EmailController));
        private readonly ISmsServices _smsServices;

        public EmailController(IChromiumSettings chromiumSettings, ISmsServices smsServices) : base(chromiumSettings)
        {
            _smsServices = smsServices;
        }

        #region GET
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
                accountData = await TryRegister(await _smsServices.GetServiceInfoList(ServiceCode.MailRu));
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
            IAccountData accountData;
            try
            {
                accountData = await TryRegister(await _smsServices.GetServiceInfoList(ServiceCode.Yandex));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(accountData);
        }

        [HttpGet]
        [Route("newGmailEmail")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewGmailEmail()
        {
            IAccountData accountData;
            try
            {
                accountData = await TryRegister(await _smsServices.GetServiceInfoList(ServiceCode.Yandex));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(accountData);
        }

        #endregion
        #region POST
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

        [HttpPost]
        [Route("checkEmail")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> PostCheckEmail([FromBody]string[] emails)
        {
            if (emails == null) return BadRequest();
            try
            {
                return Ok(await new EmailCheck(chromiumSettings: _chromiumSettings, progressLog: null).RunScenario(emails: emails));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            return InternalServerError();
        }
        #endregion

        #region private
        private async Task<IAccountData> TryRegister(IEnumerable<SmsServiceInfo> infos)
        {
            var accountData = GetRandomAccountData();
            //IAccountData accountData;
            foreach (var info in infos)
            {
                accountData = GetRandomAccountData(info.CountryCode);
                accountData.PhoneCountryCode = Enum.GetName(typeof(CountryCode), info.CountryCode);
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), info.ServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), info.SmsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await MailRegistration(accountData, info.SmsServiceCode, info.ServiceCode);
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), info.ServiceCode)}  via {Enum.GetName(typeof(SmsServiceCode), info.SmsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
                //await MailRegistration
                if (accountData == null) break;
                if (accountData.Success) break;
                if (string.IsNullOrEmpty(accountData.ErrMsg)) break;
                if (!(accountData.ErrMsg.Equals(BotMessages.NoPhoneNumberMessage)
                    || accountData.ErrMsg.Equals(BotMessages.PhoneNumberNotAcceptMessage))) break;
            }
            return accountData;
        }

        private async Task<IAccountData> MailRegistration(IAccountData accountData, SmsServiceCode smsServiceCode, ServiceCode serviceCode, CountryCode countryCode = CountryCode.RU)
        {
            try
            {
                if (string.IsNullOrEmpty(accountData.AccountName))
                {
                    accountData.AccountName = Transliteration.CyrillicToLatin($"{accountData.Firstname.ToLower()}.{accountData.Lastname.ToLower()}", Language.Russian);
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
                Log.Error(exception);
            }
            return accountData;
        } 
        #endregion
    }
}
