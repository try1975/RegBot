using Common.Service.Enums;

namespace SimSmsOrg
{
    internal class CountryService
    {
        public CountryCode CountryCode { get; set; }
        public string Country { get; set; }
        public ServiceCode ServiceCode { get; set; }
        public string Service { get; set; }
        public int NumberCount { get; set; }
        public double Price { get; set; }
    }
}