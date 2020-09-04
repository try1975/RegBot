using Newtonsoft.Json;
using System;

namespace Ip2location
{
    public class Ip2LocationInfo
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("country_name")]
        public string CountryName { get; set; }
        public string region_name { get; set; }
        public string city_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string zip_code { get; set; }
        public string time_zone { get; set; }
        public int credits_consumed { get; set; }
        public Ip2LocationCountry country { get; set; }
        public Ip2LocationTimeZoneInfo time_zone_info { get; set; }
    }

    public class Ip2LocationCountry
    {
        public string name { get; set; }
        public string alpha3_code { get; set; }
        public string numeric_code { get; set; }
        public string demonym { get; set; }
        public string flag { get; set; }
        public string capital { get; set; }
        public string total_area { get; set; }
        public string population { get; set; }
        public Ip2LocationCurrency currency { get; set; }
        [JsonProperty("language")]
        public Ip2LocationLanguage Language { get; set; }
        public string idd_code { get; set; }
        public string tld { get; set; }
        //[JsonProperty("translations")]
        //public Ip2LocationTranslations Translations { get; set; }
    }

    public class Ip2LocationCurrency
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class Ip2LocationLanguage
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        public string name { get; set; }
    }

    public class Ip2LocationTranslations
    {
        [JsonProperty("ru")]
        public string Ru { get; set; }
    }

    public class Ip2LocationTimeZoneInfo
    {
        [JsonProperty("olson")]
        public string Olson { get; set; }
        public DateTime current_time { get; set; }
        public int gmt_offset { get; set; }
        public string is_dst { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }

}
