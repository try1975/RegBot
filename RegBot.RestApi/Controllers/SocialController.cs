using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using log4net;
using PuppeteerService;
using ScenarioService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RegBot.RestApi.Controllers
{
    public class SocialController : ControllerBase
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(SocialController));

        public SocialController(IChromiumSettings chromiumSettings, ISmsServices smsServices) : base(chromiumSettings, smsServices)
        {
        }

        #region GET
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
        #endregion

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
                accountData = await Registration(accountData, smsServiceCode, serviceCode);
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
                accountData = await Registration(accountData, smsServiceCode, serviceCode);
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

        private async Task<IAccountData> Registration(IAccountData accountData, SmsServiceCode smsServiceCode, ServiceCode serviceCode, CountryCode countryCode = CountryCode.RU)
        {
            return await Registration(accountData, smsServiceCode, serviceCode, countryCode, Log);
        } 
    }
}
