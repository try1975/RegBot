namespace OnlineSimRu
{
    public class GetStateResponseItem
    {
        public int tzid { get; set; }
        public string response { get; set; }
        public string service { get; set; }
        public string number { get; set; }
        public string msg { get; set; }
        public int time { get; set; }
        public string form { get; set; }
        public int forward_status { get; set; }
        public string forward_number { get; set; }
        public int status { get; set; }
        public string[] use_service { get; set; }
    }

}