using Common.Service.Enums;

namespace GetSmsOnline
{
    public class GetSmsPrice
    {
        public GetSmsCountry ru { get; set; }
        public GetSmsCountry kz { get; set; }

        public GetSmsCountry GetByCountry(CountryCode countryCode) {
            switch (countryCode)
            {
                case CountryCode.RU:
                    return ru;
                case CountryCode.KZ:
                    return kz;
                default:
                    return null;
            }
        }

        public string GetCost(CountryCode countryCode, ServiceCode serviceCode)
        {
            var getSmsCountry = GetByCountry(countryCode);
            switch (serviceCode)
            {
                case ServiceCode.MailRu:
                    return getSmsCountry.ma.cost;
                case ServiceCode.Yandex:
                    return getSmsCountry.ya.cost;
                case ServiceCode.Gmail:
                    return getSmsCountry.go.cost;
                case ServiceCode.Other:
                    return getSmsCountry.ot.cost;
                case ServiceCode.Facebook:
                    return getSmsCountry.fb.cost;
                case ServiceCode.Vk:
                    return getSmsCountry.vk.cost;
                case ServiceCode.Ok:
                    return getSmsCountry.ok.cost;
                default:
                    return "";
            }
        }
    }

    public class GetSmsCountry
    {
        public GetSmsCost ot { get; set; } //нет в списке
        public GetSmsCost vk { get; set; } // вконтакте
        public GetSmsCost ma { get; set; } // мэйлру
        public GetSmsCost wa { get; set; } //ватсапп
        public GetSmsCost ig { get; set; } //инстаграмм
        public GetSmsCost ok { get; set; } //одноклассники
        public GetSmsCost fb { get; set; } //фэйсбук
        public GetSmsCost mm { get; set; } //микрософт
        public GetSmsCost go { get; set; } //гугл
        public GetSmsCost tg { get; set; } //телеграмм
        public GetSmsCost qq { get; set; } //
        public GetSmsCost tw { get; set; } //твитер
        public GetSmsCost vi { get; set; } //вайбер
        public GetSmsCost qw { get; set; } 
        public GetSmsCost ya { get; set; } //яндекс
        public GetSmsCost lk { get; set; }
    }

    public class GetSmsCost
    {
        public string cost { get; set; }
    }
}
