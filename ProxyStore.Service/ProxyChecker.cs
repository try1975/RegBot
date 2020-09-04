using System.Net;

namespace ProxyStore.Service
{
    public static class ProxyChecker
    {
        public static bool CheckProxy(WebProxy webProxy)
        {
            WebRequest request = WebRequest.Create("http://google.com");
            request.Proxy = webProxy;
            request.Timeout = 5000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode == HttpStatusCode.OK;
        }

        public static bool CheckProxy(string proxy)
        {
            if (string.IsNullOrEmpty(proxy)) return false;
            if (proxy.Contains("@"))
            {
                var ampSplit = proxy.Split('@');
                var userAndPassword = ampSplit[0].Split(':');
                var ipport = ampSplit[1];
                if (ipport.Split(':').Length >= 2 && userAndPassword.Length == 2)
                {
                    var username = userAndPassword[0];
                    var password = userAndPassword[1];
                    var webProxy = new WebProxy(ipport)
                    {
                        Credentials = new NetworkCredential(username, password)
                    };
                    return CheckProxy(webProxy);
                }
            }
            else
            {
                var ipport = proxy.Split(':');
                if (ipport.Length >= 2)
                {
                    var ip = ipport[0];
                    var port = ipport[1];
                    var webProxy = new WebProxy(Host: ip, Port: int.Parse(port));
                    return CheckProxy(webProxy);
                }
            }
            return false;
        }
    }
}
