using Common.Service.Enums;
using System;

namespace Common.Service
{
    public static class ServiceDomains
    {
        public static string GetDomain(ServiceCode serviceCode)
        {
            var domain = string.Empty;
            switch (serviceCode)
            {
                case ServiceCode.MailRu:
                    domain = "mail.ru";
                    break;
                case ServiceCode.Yandex:
                    domain = "yandex.ru";
                    break;
                case ServiceCode.Gmail:
                    domain = "gmail.com";
                    break;
                case ServiceCode.Other:
                    break;
                case ServiceCode.Facebook:
                    domain = "facebook.com";
                    break;
                case ServiceCode.Vk:
                    domain = "vk.com";
                    break;
                case ServiceCode.Ok:
                    domain = "ok.ru";
                    break;
                default:
                    new Exception($"No domain for serviceCode {serviceCode} in {nameof(ServiceDomains)}");
                    break;
            }
            return domain;
        }
    }
}
