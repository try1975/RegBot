using System.Threading.Tasks;

namespace IpCommon
{
    public interface IIpInfoService
    {
        Task<IpInfo> GetIpInfo(string ip);
    }
}
