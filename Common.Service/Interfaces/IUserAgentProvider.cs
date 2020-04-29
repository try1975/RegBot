using Common.Service.Enums;

namespace Common.Service.Interfaces
{
    public interface IUserAgentProvider
    {
        string GetRandomUserAgent();
        string GetRandomUserAgent(ServiceCode serviceCode);
    }
}
