using Common.Service.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Service.Interfaces
{
    public interface ISmsServices
    {
        ISmsService GetSmsService(SmsServiceCode smsServiceCode);
        Task<IEnumerable<SmsServiceInfo>> GetServiceInfoList(ServiceCode serviceCode);
        Task<IEnumerable<SmsServiceInfo>> GetServiceInfoList(SmsServiceInfoCondition smsServiceInfoCondition);
        void RemoveSmsServiceLowBalance(SmsServiceCode smsServiceCode);

        Task AddFail(SmsServiceInfo smsServiceInfo);
    }
}
