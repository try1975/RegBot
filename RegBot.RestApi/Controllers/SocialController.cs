using AccountData.Service;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Facebook.Bot;
using GetSmsOnline;
using log4net;
using NickBuhro.Translit;
using Ok.Bot;
using OnlineSimRu;
using PuppeteerService;
using ScenarioService;
using SimSmsOrg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Vk.Bot;

namespace RegBot.RestApi.Controllers
{
    public class SocialController : ControllerBase
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(SocialController));
        private readonly ISmsServices _smsServices;

        public SocialController(IChromiumSettings chromiumSettings, ISmsServices smsServices) : base(chromiumSettings)
        {
            _smsServices = smsServices;
        }

        [HttpGet]
        [Route("newFacebookAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewFacebookAccount()
        {
            IAccountData accountData;
            try
            {
                accountData = await TryRegister(await _smsServices.GetServiceInfoList(ServiceCode.Facebook));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(accountData);
        }

        [HttpGet]
        [Route("newVkAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetVkAccount()
        {
            IAccountData accountData;
            try
            {
                accountData = await TryRegister(await _smsServices.GetServiceInfoList(ServiceCode.Vk));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(accountData);
        }

        [HttpGet]
        [Route("newOkAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetOkAccount()
        {
            IAccountData accountData;
            try
            {
                accountData = await TryRegister(await _smsServices.GetServiceInfoList(ServiceCode.Ok));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(accountData);
        }

        #region POST
        [HttpPost]
        [Route("newFacebookAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostFacebookAccount(EmailAccountData data)
        {
            if (data == null) return BadRequest();
            var accountData = (IAccountData)data;
            var smsServiceCode = GetRandomSmsServiceCode();
            const ServiceCode serviceCode = ServiceCode.Facebook;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await SocialRegistration(accountData, smsServiceCode, serviceCode);
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
        [Route("newVkAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewVkAccount(EmailAccountData data)
        {
            if (data == null) return BadRequest();
            var accountData = (IAccountData)data;
            var smsServiceCode = GetRandomSmsServiceCode();
            const ServiceCode serviceCode = ServiceCode.Vk;

            try
            {
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await SocialRegistration(accountData, smsServiceCode, serviceCode);
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
        [Route("collectVkWall")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> PostCollectVkWall(string login, string password, string query, int pageCount = 10)
        {
            List<string> results;
            try
            {
                var engine = new CollectVkWall(_chromiumSettings);
                results = await engine.RunScenario(accountData: new EmailAccountData { Phone = login, Password = password }, vkAccountNames: new[] { query }, pageCount: pageCount);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(results);
        }

        [HttpPost]
        [Route("messageVkGroup")]
        public async Task<IHttpActionResult> PostMessageVkGroup(string login, string password, string group, string message)
        {
            try
            {
                var engine = new PostVk(chromiumSettings: _chromiumSettings);
                await engine.RunScenario(accountData: new EmailAccountData { Phone = login, Password = password }, vkGroups: new[] { group }, message: message);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok();
        }

        [HttpPost]
        [Route("checkVkAccount")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> PostCheckVkAccount(string login, string password, string vkAccountName)
        {
            List<string> results;
            try
            {
                var engine = new CheckVkAccount(chromiumSettings: _chromiumSettings);
                results = await engine.RunScenario(accountData: new EmailAccountData { Phone = login, Password = password }, vkAccountNames: new[] { vkAccountName });
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(results);
        }

        [HttpPost]
        [Route("checkVkCredential")]
        [ResponseType(typeof(List<LoginPasswordInput>))]
        public async Task<IHttpActionResult> PostCheckVkCredential([FromBody]List<LoginPasswordInput> listLoginPasswordInput)
        {
            try
            {
                return Ok(await new CheckVkCredential(chromiumSettings: _chromiumSettings).RunScenario(listLoginPasswordInput));
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
                accountData = await SocialRegistration(accountData, info.SmsServiceCode, info.ServiceCode);
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

        private async Task<IAccountData> SocialRegistration(IAccountData accountData, SmsServiceCode smsServiceCode, ServiceCode serviceCode, CountryCode countryCode = CountryCode.RU)
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
                Log.Error(exception);
            }
            return accountData;
        } 
        #endregion
    }
}
