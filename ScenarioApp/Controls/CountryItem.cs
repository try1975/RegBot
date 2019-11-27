using System;
using System.Collections.Generic;
using System.Linq;
using Common.Service.Enums;

namespace ScenarioApp.Controls
{
    public class CountryItem
    {
        public string Text { get; set; }
        public CountryCode CountryCode { get; set; }

        public static List<CountryItem> GetCountryItems()
        {

            var list = new List<CountryItem>(Enum.GetNames(typeof(CountryCode)).Length);
            var values = Enum.GetValues(typeof(CountryCode)).Cast<CountryCode>();
            list.AddRange(values.Select(countryCode => new CountryItem { Text = Common.Service.Utils.GetDescription(countryCode), CountryCode = countryCode }));
            return list;
        }
    }
}