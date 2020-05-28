using Common.Service.Enums;
using System.Collections.Generic;

namespace Common.Service
{
    public class SmsServiceInfo
    {
        public SmsServiceCode SmsServiceCode { get; set; }
        public CountryCode CountryCode { get; set; }
        public ServiceCode ServiceCode { get; set; }
        public int NumberCount { get; set; }
        public double Price { get; set; }
        public bool Skiped { get; set; }
        public int FailCount { get; set; }
    }

    public class SmsServiceInfoCondition
    {
        public ServiceCode ServiceCode { get; set; }
        public List<CountryCode> CountryCodes { get; set; }
    }
}