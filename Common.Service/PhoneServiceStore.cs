using System.Collections.Generic;
using Common.Service.Enums;

namespace Common.Service
{
    public static class PhoneServiceStore
    {
        public static readonly Dictionary<CountryCode, string> CountryPrefixes = new Dictionary<CountryCode,string>();
        static PhoneServiceStore()
        {
            CountryPrefixes[CountryCode.US] = "1";
            CountryPrefixes[CountryCode.RU] = "7";
            CountryPrefixes[CountryCode.EG] = "20";
            CountryPrefixes[CountryCode.ZA] = "27";
            CountryPrefixes[CountryCode.NL] = "31";
            CountryPrefixes[CountryCode.FR] = "33";
            CountryPrefixes[CountryCode.ES] = "34";
            CountryPrefixes[CountryCode.RO] = "40";
            CountryPrefixes[CountryCode.AT] = "43";
            CountryPrefixes[CountryCode.EN] = "44";
            CountryPrefixes[CountryCode.SE] = "46";
            CountryPrefixes[CountryCode.PL] = "48";
            CountryPrefixes[CountryCode.DE] = "49";
            CountryPrefixes[CountryCode.ID] = "62";
            CountryPrefixes[CountryCode.PH] = "63";
            CountryPrefixes[CountryCode.KZ] = "77";
            CountryPrefixes[CountryCode.CN] = "86";
            CountryPrefixes[CountryCode.MM] = "95";

            CountryPrefixes[CountryCode.CI] = "225";
            CountryPrefixes[CountryCode.GH] = "233";
            CountryPrefixes[CountryCode.NG] = "234";
            CountryPrefixes[CountryCode.TD] = "235";
            CountryPrefixes[CountryCode.CM] = "237";
            CountryPrefixes[CountryCode.KE] = "254";

            CountryPrefixes[CountryCode.CY] = "357";
            CountryPrefixes[CountryCode.LT] = "370";
            CountryPrefixes[CountryCode.LV] = "371";
            CountryPrefixes[CountryCode.EE] = "372";
            CountryPrefixes[CountryCode.MD] = "373";
            CountryPrefixes[CountryCode.BY] = "375";
            CountryPrefixes[CountryCode.UA] = "380";
            CountryPrefixes[CountryCode.RS] = "381";

            CountryPrefixes[CountryCode.CZ] = "420";
            CountryPrefixes[CountryCode.HT] = "509";

            CountryPrefixes[CountryCode.YE] = "967";
            CountryPrefixes[CountryCode.GE] = "995";
            CountryPrefixes[CountryCode.KG] = "996";
            CountryPrefixes[CountryCode.UZ] = "998";
            CountryPrefixes[CountryCode.CA] = "1000";
        }
    }
}