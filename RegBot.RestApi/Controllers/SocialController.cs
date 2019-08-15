using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Facebook.Bot;
using GetSmsOnline;
using log4net;
using NickBuhro.Translit;
using OnlineSimRu;
using SimSmsOrg;

namespace RegBot.RestApi.Controllers
{
    public class SocialController : ControllerBase
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(SocialController));

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

        [HttpPost]
        [Route("newFacebookAccount")]
        [ResponseType(typeof(IAccountData))]
        public async Task<IHttpActionResult> PostNewMaNewFacebookAccountilRuEmail(EmailAccountData data)
        {
            if (data == null) return BadRequest();
            var accountData = (IAccountData)data;
            var smsServiceCode = GetRandomSmsServiceCode();
            const ServiceCode serviceCode = ServiceCode.MailRu;

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
                        iBot = new FacebookRegistration(accountData, smsService, AppPath);
                        break;
                    //case ServiceCode.Vk:
                    //    iBot = new VkRegistration(accountData, smsService, AppPath);
                    //    break;
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

                accountData = await iBot.Registration(countryCode, headless: false);
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
