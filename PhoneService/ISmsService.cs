using System.Threading.Tasks;
using Common.Service;

namespace Phone.Service
{
    public interface ISmsService
    {
        Task<PhoneNumberRequest> GetPhoneNumber(CountryCode countryCode, MailServiceCode mailServiceCode);
        Task<PhoneNumberValidation> GetSmsValidation(string id);
        Task SetSmsValidationSuccess(string id);
    }
}