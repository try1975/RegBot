using Common.Service.Enums;
using System.Collections.Generic;

namespace Common.Service
{
    public static class ServiceDomains
    {
        public static Dictionary<ServiceCode, string> domains;
        static ServiceDomains()
        {
            domains = new Dictionary<ServiceCode, string>() {
                { ServiceCode.MailRu, "mail.ru" },
                { ServiceCode.Yandex, "yandex.ru" },
                { ServiceCode.Gmail, "gmail.com" },
                { ServiceCode.Other, string.Empty },
                { ServiceCode.Facebook, "facebook.com" },
                { ServiceCode.Vk, "vk.com" },
                { ServiceCode.Instagram, "instagram.com" },
                { ServiceCode.Twitter, "twitter.com" },
                { ServiceCode.Telegram, "telegram.org" }
            };
        }
        public static string GetDomain(ServiceCode serviceCode) => domains.TryGetValue(serviceCode, out string domain) ? domain : string.Empty;
    }
}
