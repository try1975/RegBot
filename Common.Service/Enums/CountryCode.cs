using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Service.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CountryCode
    {
        [EnumLocalize("Россия")]
        RU,
        [EnumLocalize("Казахстан")]
        KZ,
        [EnumLocalize("Китай")]
        CN,
        [EnumLocalize("Германия")]
        DE,
        [EnumLocalize("Великобритания")]
        EN,
        [EnumLocalize("Нидерланды")]
        NL,
        [EnumLocalize("Франция")]
        FR,
        [EnumLocalize("Украина")]
        UA,
        [EnumLocalize("Киргизия")]
        KG,
        [EnumLocalize("Узбекистан")]
        UZ,
        [EnumLocalize("Сербия")]
        RS,
        [EnumLocalize("Молдова")]
        MD,
        [EnumLocalize("Польша")]
        PL,
        [EnumLocalize("Австрия")]
        AT,
        [EnumLocalize("Латвия")]
        LV,
        [EnumLocalize("Эстония")]
        EE,
        [EnumLocalize("Египет")]
        EG,
        [EnumLocalize("Нигерия")]
        NG,
        [EnumLocalize("Гаити")]
        HT,
        [EnumLocalize("Кот-д’Ивуар")]
        CI,
        [EnumLocalize("Йемен")]
        YE,
        [EnumLocalize("Камерун")]
        CM,
        [EnumLocalize("Чад")]
        TD
    }
}