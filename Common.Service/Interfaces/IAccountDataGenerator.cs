using Common.Service.Enums;

namespace Common.Service.Interfaces
{
    public interface IAccountDataGenerator
    {
        IAccountData GetRandom(CountryCode countryCode);
        IAccountData GetRandomMale(CountryCode countryCode);
        IAccountData GetRandomFemale(CountryCode countryCode);
    }
}