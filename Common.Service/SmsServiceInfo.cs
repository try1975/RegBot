using Common.Service.Enums;

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
    }
}