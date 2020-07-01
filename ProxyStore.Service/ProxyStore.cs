using Common.Service.Enums;
using Common.Service.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProxyStore.Service
{
    public class ProxyStore : IProxyStore
    {
        //use files instead litedb
        private readonly Random _random = new Random();
        private readonly string _connectionString;
        private const string CollectionName = "Proxies";
        private readonly List<IProxyData> list = new List<IProxyData>();
        private readonly string _path;

        public ProxyStore(string connectionString, string path)
        {
            _connectionString = connectionString;
            //using (var db = new LiteDatabase(_connectionString))
            //{
            //    list = db.GetCollection<IProxyData>(CollectionName)
            //    //.Find(Query.And(Query.EQ(nameof(IProxyData.Domain), ServiceDomains.GetDomain(ServiceCode.Gmail)),
            //    //Query.EQ(nameof(IProxyData.Success), true)
            //    //))
            //    .FindAll()
            //    .ToList()
            //    ;
            //}

            _path = Path.Combine(path, "Data", "proxies.txt");

            if (File.Exists(_path))
            {
                var proxies = File.ReadAllLines(_path).ToList();
                var time = File.GetLastWriteTime(_path).AddHours(12);
                if (time < DateTime.Now)
                {
                    List<WebProxy> lowp = new List<WebProxy>();
                    foreach (var proxy in proxies)
                    {
                        if (string.IsNullOrEmpty(proxy)) continue;
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
                                lowp.Add(webProxy);
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
                                lowp.Add(webProxy);
                            }
                        }
                    }
                    //list.AddRange(JsonConvert.DeserializeObject<List<IProxyData>>(File.ReadAllText(path)));
                    proxies.Clear();
                    CheckProxies(lowp, proxies);
                    File.WriteAllLines(_path, proxies);
                }
                foreach (var goodProxy in proxies)
                {
                    list.Add(new ProxyData { ProxyString = goodProxy });
                }
            }
        }

        public string GetProxy(ServiceCode serviceCode)
        {
            //return string.Empty;
            if (!list.Any()) return string.Empty;
            var idx = _random.Next(0, list.Count - 1);
            return list[idx].ProxyString;
        }

        public IProxyData StoreItem(IProxyData proxyData)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<IProxyData>(CollectionName);
                if (proxyData.Id != 0)
                {
                    col.Update(proxyData);
                }
                else
                {
                    var id = col.Insert(proxyData).AsInt32;
                    proxyData.Id = id;
                    //proxyData.CreatedAt = DateTime.Now;
                }
            }
            return proxyData;
        }

        public bool RemoveItem(int id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<IProxyData>(CollectionName);
                return col.Delete(x => x.Id == id) == 1 ? true : false;
            }
        }

        private static bool CheckProxy(WebProxy webProxy)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://google.com");
                request.Proxy = webProxy;
                request.Timeout = 5000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void CheckProxies(List<WebProxy> lowp, List<string> goodProxies)
        {
            Parallel.ForEach(lowp, wp =>
            {
                if (CheckProxy(wp))
                {
                    var proxy = $"{wp.Address.Host}:{wp.Address.Port}";
                    if (wp.Credentials != null)
                    {
                        var сredentials = wp.Credentials as NetworkCredential;
                        proxy = $"{сredentials.UserName}:{сredentials.Password}@{proxy}";
                    }
                    goodProxies.Add($"{proxy}");
                }

            });
        }

        public void MarkProxySuccess(ServiceCode serviceCode, string proxy)
        {
            //
        }

        public void MarkProxyFail(ServiceCode serviceCode, string proxy)
        {
            list.RemoveAll(x => x.ProxyString.Equals(proxy));
        }

        public List<IProxyData> GetProxies()
        {
            return list;
        }

        public string GetPath()
        {
            return _path;
        }
    }


}
