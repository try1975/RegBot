using Common.Service.Enums;
using Common.Service.Interfaces;
using GetSmsOnline;
using OnlineSimRu;
using SimSmsOrg;

namespace RegBot.Demo
{
    public class SmsServiceItem
    {
        public string Text { get; set; }
        public ISmsService SmsService { get; set; }

        public static SmsServiceItem[] GetSmsServiceItems()
        {
            return new[] {
                new SmsServiceItem { Text=Common.Service.Utils.GetDescription(SmsServiceCode.GetSmsOnline), SmsService = new GetSmsOnlineApi()},
                new SmsServiceItem { Text=Common.Service.Utils.GetDescription(SmsServiceCode.OnlineSimRu), SmsService = new OnlineSimRuApi()},
                new SmsServiceItem { Text=Common.Service.Utils.GetDescription(SmsServiceCode.SimSmsOrg), SmsService = new SimSmsOrgApi()}
            };
        }
    }
}