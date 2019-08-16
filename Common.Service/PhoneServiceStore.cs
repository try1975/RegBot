using System.Collections.Generic;
using Common.Service.Enums;

namespace Common.Service
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
            CountryPrefixes[CountryCode.KG] = "996";
            CountryPrefixes[CountryCode.UZ] = "998";
            CountryPrefixes[CountryCode.RS] = "381";
            CountryPrefixes[CountryCode.MD] = "373";
            CountryPrefixes[CountryCode.PL] = "48";
            CountryPrefixes[CountryCode.AT] = "43";
            CountryPrefixes[CountryCode.LV] = "371";
            CountryPrefixes[CountryCode.EE] = "372";
            CountryPrefixes[CountryCode.EG] = "20";
            CountryPrefixes[CountryCode.NG] = "234";
            CountryPrefixes[CountryCode.HT] = "509";
            CountryPrefixes[CountryCode.CI] = "225";
            CountryPrefixes[CountryCode.YE] = "967";
            CountryPrefixes[CountryCode.CM] = "237";
            CountryPrefixes[CountryCode.TD] = "235";
            CountryPrefixes[CountryCode.KZ] = "77";
        }
    }
}