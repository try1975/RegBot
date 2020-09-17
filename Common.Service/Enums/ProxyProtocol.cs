using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Service.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProxyProtocol { HTTP, HTTPS, SOCKS4, SOCKS5}
}
