using System.Threading.Tasks;
using Common.Service.Enums;

namespace Common.Service.Interfaces
{
    public interface ISmsService
    {
        Task<PhoneNumberRequest> GetPhoneNumber(CountryCode countryCode, MailServiceCode mailServiceCode);
        Task<PhoneNumberValidation> GetSmsValidation(string id);
        Task SetSmsValidationSuccess(string id);
    }
}