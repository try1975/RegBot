using Common.Service.Enums;
using Common.Service.Interfaces;
using GetSmsOnline;
using OnlineSimRu;
using SimSmsOrg;

namespace ScenarioApp.Controls
{
    public class SmsServiceItem
    {
        public string Text { get; set; }
        public ISmsService SmsService { get; set; }
        public SmsServiceCode SmsServiceCode { get; set; }

        public static SmsServiceItem[] GetSmsServiceItems()
        {
            return new[] {
                new SmsServiceItem
                {
                    Text = Common.Service.Utils.GetDescription(SmsServiceCode.OnlineSimRu),
                    SmsService = new OnlineSimRuApi(),
                    SmsServiceCode = SmsServiceCode.OnlineSimRu
                },
                new SmsServiceItem
                {
                    Text = Common.Service.Utils.GetDescription(SmsServiceCode.GetSmsOnline),
                    SmsService = new GetSmsOnlineApi(),
                    SmsServiceCode = SmsServiceCode.GetSmsOnline
                },
                new SmsServiceItem
                {
                    Text = Common.Service.Utils.GetDescription(SmsServiceCode.SimSmsOrg),
                    SmsService = new SimSmsOrgApi(),
                    SmsServiceCode = SmsServiceCode.SimSmsOrg
                }
            };
        }
    }
}