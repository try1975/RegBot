using System.Collections.Generic;
using Common.Service;

namespace Phone.Service
{
    public static class PhoneServiceStore
    {
        public static readonly Dictionary<CountryCode, string> CountryPrefixes = new Dictionary<CountryCode,string>();
        static PhoneServiceStore()
        {
            CountryPrefixes[CountryCode.RU] = "7";
            CountryPrefixes[CountryCode.CN] = "86";
            CountryPrefixes[CountryCode.DE] = "49";
            CountryPrefixes[CountryCode.NL] = "31";
            CountryPrefixes[CountryCode.EN] = "44";
            CountryPrefixes[CountryCode.FR] = "33";
            CountryPrefixes[CountryCode.UA] = "380";
            CountryPrefixes[CountryCode.KZ] = "77";
            /*
996 - Киргизия
998 - Узбекистан
381 - Сербия
373 - Молдова
48 - Польша
43 - Aвстрия
371 - Латвия
372 - Эстония
20 - Египет
234 - Нигерия
509 - Гаити
225 - Кот-д’Ивуар
967 - Йемен
237 - Камерун
235 - Чад
             */
        }
    }
}