using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Service.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SexCode
    {
        Male,
        Female
    }
}