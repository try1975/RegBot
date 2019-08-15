using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Service.Enums;

namespace Common.Service.Interfaces
{
    public interface ISmsService
    {
        Task<PhoneNumberRequest> GetPhoneNumber(CountryCode countryCode, ServiceCode serviceCode);
        Task<PhoneNumberValidation> GetSmsValidation(string id);
        Task SetSmsValidationSuccess(string id);
        Task SetNumberFail(string id);
        Task<List<SmsServiceInfo>> GetInfo();
    }
}