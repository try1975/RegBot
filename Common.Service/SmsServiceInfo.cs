using Common.Service.Enums;

namespace Common.Service
{
    public class SmsServiceInfo
    {
        public string SmsServiceCode { get; set; }
        public string CountryCode { get; set; }
        public string MailServiceCode { get; set; }
        public int NumberCount { get; set; }
        public double Price { get; set; }
    }
}