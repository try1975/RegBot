using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Service
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CountryCode
    {
        RU,
        KZ,
        CN,
        DE,
        EN,
        NL,
        FR,
        UA
    }
}