using Common.Service.Enums;
using System.Collections.Generic;

namespace Common.Service
{
    public static class CountryGroup
    {
        private static readonly List<CountryCode> _europe = new List<CountryCode>();
        private static readonly List<CountryCode> _westEurope = new List<CountryCode>();
        private static readonly List<CountryCode> _eastEurope = new List<CountryCode>();

        static CountryGroup()
        {
            _westEurope.AddRange(new[] { CountryCode.AT, CountryCode.BE, CountryCode.EN, CountryCode.GE, CountryCode.IE, CountryCode.NL, CountryCode.FR, CountryCode.CH });
            _eastEurope.AddRange(new[] { CountryCode.BY, CountryCode.BG, CountryCode.HU });

            _europe.AddRange(_westEurope);
            _europe.AddRange(_eastEurope);
        }

        public static List<CountryCode> Europe => _europe;
        public static List<CountryCode> WestEurope => _westEurope;
        public static List<CountryCode> EastEurope => _eastEurope;
    }
}
