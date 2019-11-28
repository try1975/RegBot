using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Facebook.Bot;
using GetSmsOnline;
using log4net;
using NickBuhro.Translit;
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

        public SocialController(IChromiumSettings chromiumSettings) : base(chromiumSettings)
        {
        }

        [HttpGet]
        [Route("newFacebookAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetNewFacebookAccount()
        {
            IAccountData accountData;
            try
            {
                accountData = GetRandomAccountData();
                var smsServiceCode = GetRandomSmsServiceCode();
                const ServiceCode serviceCode = ServiceCode.Facebook;

                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await SocialRegistration(accountData, smsServiceCode, serviceCode);
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
        [Route("newVkAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> GetVkAccount()
        {
            IAccountData accountData;
            try
            {
                accountData = GetRandomAccountData();
                var smsServiceCode = GetRandomSmsServiceCode();
                const ServiceCode serviceCode = ServiceCode.Vk;

                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                accountData = await SocialRegistration(accountData, smsServiceCode, serviceCode);
                Log.Debug($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}  via {Enum.GetName(typeof(SmsServiceCode), smsServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(accountData);
        }

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
        [ResponseType(typeof(List<CheckVkCredentialInput>))]
        public async Task<IHttpActionResult> PostCheckVkCredential([FromBody]List<CheckVkCredentialInput> listCheckVkCredentialInput)
        {
            try
            {
                //var scenario = new CheckVkCredential(chromiumSettings: _chromiumSettings);
                return Ok(await new CheckVkCredential(chromiumSettings: _chromiumSettings).RunScenario(listCheckVkCredentialInput));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            return InternalServerError();
        }

        private async Task<IAccountData> SocialRegistration(IAccountData accountData, SmsServiceCode smsServiceCode, ServiceCode serviceCode)
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
                    case ServiceCode.Facebook:
                        iBot = new FacebookRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    case ServiceCode.Vk:
                        iBot = new VkRegistration(accountData, smsService, _chromiumSettings);
                        break;
                    //case ServiceCode.Ok:
                    //    iBot = new GmailRegistration(accountData, smsService, AppPath);
                    //    break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var countryCode = CountryCode.RU;
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
    }
}
