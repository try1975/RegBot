using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AccountData.Service
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SexEnum
    {
        Male,
        Female
    }
}