using GetSmsOnline;
using OnlineSimRu;
using Phone.Service;

namespace RegBot.Demo
{
    public class SmsServiceItem
    {
        public string Text { get; set; }
        public ISmsService SmsService { get; set; }

        public static SmsServiceItem[] GetSmsServiceItems()
        {
            return new[] {
                new SmsServiceItem { Text=Common.Service.Utils.GetDescription(Common.Service.SmsServiceCode.GetSmsOnline), SmsService=new GetSmsOnlineApi()},
                new SmsServiceItem { Text=Common.Service.Utils.GetDescription(Common.Service.SmsServiceCode.OnlineSimRu), SmsService=new OnlineSimRuApi()}
            };
        }
    }
}