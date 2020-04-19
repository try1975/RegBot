namespace OnlineSimRu
{
    public class OnlineSimRuStatResponse
    {
        public string name { get; set; }
        public int? position { get; set; }
        public int? code { get; set; }
        public bool other { get; set; }
        public bool _new { get; set; }
        public bool enabled { get; set; }
        public OnlineSimRuStatResponseServices services { get; set; }
    }

    public class OnlineSimRuStatResponseServices
    {
        public OnlineSimRuStatResponseService mailru { get; set; }
        public OnlineSimRuStatResponseService google { get; set; }
        public OnlineSimRuStatResponseService yandex { get; set; }
    }

    public class OnlineSimRuStatResponseService
    {
        public string count { get; set; }
        public string price { get; set; }
    }

}