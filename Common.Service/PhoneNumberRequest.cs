using System;

namespace Common.Service
{
    public class PhoneNumberRequest
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public int ActiveSeconds { get; set; }
        public int RemainSeconds { get; set; }
    }
}