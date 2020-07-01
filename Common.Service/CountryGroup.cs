using Common.Service.Enums;
using System.Collections.Generic;

namespace Common.Service
{
    public static class CountryGroup
    {
        private static readonly List<CountryCode> _europe = new List<CountryCode>();
        private static readonly List<CountryCode> _westEurope = new List<CountryCode>();
        private static readonly List<CountryCode> _eastEurope = new List<CountryCode>();
        private static readonly List<CountryCode> _northEurope = new List<CountryCode>();
        private static readonly List<CountryCode> _southEurope = new List<CountryCode>();
        private static readonly List<CountryCode> _asia = new List<CountryCode>();
        private static readonly List<CountryCode> _westAsia = new List<CountryCode>();
        private static readonly List<CountryCode> _eastAsia = new List<CountryCode>();

        static CountryGroup()
        {
            _westEurope.AddRange(new[] { CountryCode.AT, CountryCode.BE, CountryCode.EN, CountryCode.GE, CountryCode.IE, CountryCode.NL, CountryCode.FR, CountryCode.CH });
            _eastEurope.AddRange(new[] { CountryCode.BY, CountryCode.BG, CountryCode.HU, CountryCode.MD, CountryCode.PL, CountryCode.RU, CountryCode.RO, CountryCode.SK, CountryCode.CZ, CountryCode.UA });
            _northEurope.AddRange(new[] { CountryCode.DK, CountryCode.IS, CountryCode.LV, CountryCode.LT, CountryCode.NO, CountryCode.FI, CountryCode.EE, CountryCode.SE });
            _southEurope.AddRange(new[] { CountryCode.ES, CountryCode.RS, CountryCode.SI, CountryCode.HR });

            _europe.AddRange(_westEurope);
            _europe.AddRange(_eastEurope);
            _europe.AddRange(_northEurope);
            _europe.AddRange(_southEurope);

            _westAsia.AddRange(new[] { CountryCode.AZ, CountryCode.GE, CountryCode.EG, CountryCode.IL });
            _eastAsia.AddRange(new[] { CountryCode.CN, CountryCode.TW });
            _asia.AddRange(_westAsia);
            _asia.AddRange(_eastAsia);
        }

        public static List<CountryCode> Europe => _europe;
        public static List<CountryCode> WestEurope => _westEurope;
        public static List<CountryCode> EastEurope => _eastEurope;
        public static List<CountryCode> NorthEurope => _northEurope;
        public static List<CountryCode> SouthEurope => _southEurope;
        public static List<CountryCode> Asia => _asia;
    }
}
