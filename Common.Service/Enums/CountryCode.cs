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
        [EnumLocalize("Аргентина")] AR,
        [EnumLocalize("Беларусь")] BY,
        [EnumLocalize("Бразилия")] BR,
        [EnumLocalize("Великобритания")] EN,
        [EnumLocalize("Гаити")] HT,
        [EnumLocalize("Гамбия")] GM,
        [EnumLocalize("Гана")] GH,
        [EnumLocalize("Германия")] DE,
        [EnumLocalize("Грузия")] GE,
        [EnumLocalize("Египет")] EG,
        [EnumLocalize("Израиль")] IL,
        [EnumLocalize("Индонезия")] ID,
        [EnumLocalize("Ирак")] IQ,
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
        [EnumLocalize("Молдова")] MD,
        [EnumLocalize("Мьянма")] MM,
        [EnumLocalize("Нигерия")] NG,
        [EnumLocalize("Нидерланды")] NL,
        [EnumLocalize("Парагвай")] PY,
        [EnumLocalize("Польша")] PL,
        [EnumLocalize("Румыния")] RO,
        [EnumLocalize("США")] US,
        [EnumLocalize("Сербия")] RS,
        [EnumLocalize("Таиланд")] TH,
        [EnumLocalize("Узбекистан")] UZ,
        [EnumLocalize("Украина")] UA,
        [EnumLocalize("Филиппины")] PH,
        [EnumLocalize("Финляндия")] FI,
        [EnumLocalize("Франция")] FR,
        [EnumLocalize("Хорватия")] HR,
        [EnumLocalize("Чад")] TD,
        [EnumLocalize("Чехия")] CZ,
        [EnumLocalize("Швеция")] SE,
        [EnumLocalize("Эстония")] EE,
        [EnumLocalize("ЮАР")] ZA,

    }
}