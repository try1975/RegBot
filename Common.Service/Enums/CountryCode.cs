﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Service.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CountryCode
    {
        [EnumLocalize("Россия")] RU,
        [EnumLocalize("Австрия")] AT,
        [EnumLocalize("Азербайджан")] AZ,
        [EnumLocalize("Алжир")] DZ,
        [EnumLocalize("Аргентина")] AR,
        [EnumLocalize("Бангладеш")] BD,
        [EnumLocalize("Беларусь")] BY,
        [EnumLocalize("Бельгия")] BE,
        [EnumLocalize("Болгария")] BG,
        [EnumLocalize("Бразилия")] BR,
        [EnumLocalize("Великобритания")] EN,
        [EnumLocalize("Венгрия")] HU,
        [EnumLocalize("Гаити")] HT,
        [EnumLocalize("Гамбия")] GM,
        [EnumLocalize("Гана")] GH,
        [EnumLocalize("Германия")] DE,
        [EnumLocalize("Грузия")] GE,
        [EnumLocalize("Дания")] DK,
        [EnumLocalize("Египет")] EG,
        [EnumLocalize("Израиль")] IL,
        [EnumLocalize("Индонезия")] ID,
        [EnumLocalize("Ирак")] IQ,
        [EnumLocalize("Иран")] IR,
        [EnumLocalize("Ирландия")] IE,
        [EnumLocalize("Испания")] ES,
        [EnumLocalize("Йемен")] YE,
        [EnumLocalize("Казахстан")] KZ,
        [EnumLocalize("Камбоджа")] KH,
        [EnumLocalize("Камерун")] CM,
        [EnumLocalize("Канада")] CA,
        [EnumLocalize("Кения")] KE,
        [EnumLocalize("Кипр")] CY,
        [EnumLocalize("Киргизия")] KG,
        [EnumLocalize("Китай")] CN,
        [EnumLocalize("Кот-д’Ивуар")] CI,
        [EnumLocalize("Лаос")] LA,
        [EnumLocalize("Латвия")] LV,
        [EnumLocalize("Литва")] LT,
        [EnumLocalize("Марокко")] MA,
        [EnumLocalize("Мексика")] MX,
        [EnumLocalize("Молдова")] MD,
        [EnumLocalize("Мьянма")] MM,
        [EnumLocalize("Нигерия")] NG,
        [EnumLocalize("Нидерланды")] NL,
        [EnumLocalize("Парагвай")] PY,
        [EnumLocalize("Польша")] PL,
        [EnumLocalize("Румыния")] RO,
        [EnumLocalize("Саудовская Аравия")] SA,
        [EnumLocalize("Сенегал")] SN,
        [EnumLocalize("Сербия")] RS,
        [EnumLocalize("Словакия")] SK,
        [EnumLocalize("Словения")] SI,
        [EnumLocalize("США")] US,
        [EnumLocalize("Таиланд")] TH,
        [EnumLocalize("Тайвань")] TW,
        [EnumLocalize("Турция")] TR,
        [EnumLocalize("Узбекистан")] UZ,
        [EnumLocalize("Украина")] UA,
        [EnumLocalize("Филиппины")] PH,
        [EnumLocalize("Финляндия")] FI,
        [EnumLocalize("Франция")] FR,
        [EnumLocalize("Хорватия")] HR,
        [EnumLocalize("Чад")] TD,
        [EnumLocalize("Чехия")] CZ,
        [EnumLocalize("Швейцария")] CH,
        [EnumLocalize("Швеция")] SE,
        [EnumLocalize("Эстония")] EE,
        [EnumLocalize("ЮАР")] ZA,

    }
}