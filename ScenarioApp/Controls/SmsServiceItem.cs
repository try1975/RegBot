using Common.Service.Enums;

namespace ScenarioApp.Controls
{
    public class SmsServiceItem
    {
        public string Text { get; set; }
        public SmsServiceCode SmsServiceCode { get; set; }

        public static SmsServiceItem[] GetSmsServiceItems()
        {
            return new[] {
                new SmsServiceItem
                {
                    Text = Common.Service.Utils.GetDescription(SmsServiceCode.OnlineSimRu),
                    SmsServiceCode = SmsServiceCode.OnlineSimRu
                },
                new SmsServiceItem
                {
                    Text = Common.Service.Utils.GetDescription(SmsServiceCode.GetSmsOnline),
                    SmsServiceCode = SmsServiceCode.GetSmsOnline
                },
                new SmsServiceItem
                {
                    Text = Common.Service.Utils.GetDescription(SmsServiceCode.SimSmsOrg),
                    SmsServiceCode = SmsServiceCode.SimSmsOrg
                }
            };
        }
    }
}