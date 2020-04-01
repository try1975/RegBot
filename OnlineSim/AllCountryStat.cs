using Newtonsoft.Json;

namespace OnlineSimRu
{
    public class AllCountryStat
    {
        [JsonProperty("7")]
        public CountryStat RU { get; set; }
        [JsonProperty("380")]
        public CountryStat UA { get; set; }
        [JsonProperty("49")]
        public CountryStat DE { get; set; }
        [JsonProperty("46")]
        public CountryStat SE { get; set; }
        [JsonProperty("357")]
        public CountryStat CY { get; set; }
        [JsonProperty("44")]
        public CountryStat EN { get; set; }
        [JsonProperty("34")]
        public CountryStat ES { get; set; }
        [JsonProperty("33")]
        public CountryStat FR { get; set; }
        [JsonProperty("1")]
        public CountryStat US { get; set; }
        [JsonProperty("1000")]
        public CountryStat CA { get; set; }
        [JsonProperty("62")]
        public CountryStat ID { get; set; }
        [JsonProperty("63")]
        public CountryStat PH { get; set; }
        [JsonProperty("86")]
        public CountryStat CN { get; set; }
        [JsonProperty("77")]
        public CountryStat KZ { get; set; }
        [JsonProperty("40")]
        public CountryStat RO { get; set; }
        [JsonProperty("48")]
        public CountryStat PL { get; set; }
        [JsonProperty("420")]
        public CountryStat CZ { get; set; }
        [JsonProperty("43")]
        public CountryStat AT { get; set; }
        [JsonProperty("371")]
        public CountryStat LV { get; set; }
        [JsonProperty("370")]
        public CountryStat LT { get; set; }
        [JsonProperty("372")]
        public CountryStat EE { get; set; }
        [JsonProperty("998")]
        public CountryStat UZ { get; set; }
        [JsonProperty("381")]
        public CountryStat RS { get; set; }
        [JsonProperty("373")]
        public CountryStat MD { get; set; }
        [JsonProperty("27")]
        public CountryStat ZA { get; set; }
        [JsonProperty("95")]
        public CountryStat MM { get; set; }
        [JsonProperty("509")]
        public CountryStat HT { get; set; }
        [JsonProperty("233")]
        public CountryStat GH { get; set; }
        [JsonProperty("967")]
        public CountryStat YE { get; set; }
        [JsonProperty("234")]
        public CountryStat NG { get; set; }
        [JsonProperty("225")]
        public CountryStat CI { get; set; }
        [JsonProperty("254")]
        public CountryStat KE { get; set; }
        [JsonProperty("995")]
        public CountryStat GE { get; set; }
        [JsonProperty("375")]
        public CountryStat BY { get; set; }
    }

    public class CountryStat
    {
        public string name { get; set; }
        public string position { get; set; }
        public string code { get; set; }
        public string other { get; set; }
        public bool _new { get; set; }
        public bool enabled { get; set; }
        public Services services { get; set; }
    }

    public class Services
    {
        public Service vkcom { get; set; }
        public Service mailru { get; set; }
        public Service odklru { get; set; }
        public Service google { get; set; }
        public Service yandex { get; set; }
        [JsonProperty("3223")]
        public Service facebook { get; set; }
        public Service viber { get; set; }
        public Service whatsapp { get; set; }
        public Service telegram { get; set; }
        public Service instagram { get; set; }
        public Service microsoft { get; set; }
    }

    public class Service
    {
        public string count { get; set; }
        public bool popular { get; set; }
        public string price { get; set; }
        public string id { get; set; }
        public string service { get; set; }
        public string slug { get; set; }
    }
}
