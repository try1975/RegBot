using System.Threading.Tasks;
using AccountData.Service;
using Common.Service;

namespace Common.Bot
{
    public interface IBot
    {
        Task<IAccountData> Registration(CountryCode countryCode);
    }
}
