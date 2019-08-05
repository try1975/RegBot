using System.Threading.Tasks;
using Common.Service.Enums;

namespace Common.Service.Interfaces
{
    public interface IBot
    {
        Task<IAccountData> Registration(CountryCode countryCode, bool headless);
    }
}
