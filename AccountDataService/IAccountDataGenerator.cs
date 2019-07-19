using Common.Service;

namespace AccountData.Service
{
    public interface IAccountDataGenerator
    {
        IAccountData GetRandom(CountryCode countryCode);
        IAccountData GetRandomMale(CountryCode countryCode);
        IAccountData GetRandomFemale(CountryCode countryCode);
    }
}