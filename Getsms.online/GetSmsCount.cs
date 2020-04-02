using Common.Service.Enums;
using Newtonsoft.Json;

namespace GetSmsOnline
{
    public class GetSmsCount
    {
        [JsonProperty("ot_0")]
        public int Other { get; set; }
        [JsonProperty("vk_0")]
        public int Vk { get; set; }
        public int ma_0 { get; set; }
        public int ok_0 { get; set; }
        public int fb_0 { get; set; }
        public int mm_0 { get; set; }
        public int go_0 { get; set; }
        public int ya_0 { get; set; }

        public int GetCount(ServiceCode serviceCode) {
            switch (serviceCode)
            {
                case ServiceCode.MailRu:
                    return ma_0;
                case ServiceCode.Yandex:
                    return ya_0;
                case ServiceCode.Gmail:
                    return go_0;
                case ServiceCode.Other:
                    return Other;
                case ServiceCode.Facebook:
                    return fb_0;
                case ServiceCode.Vk:
                    return Vk;
                case ServiceCode.Ok:
                    return ok_0;
                default:
                    return 0;
            }
        }
    }

}
