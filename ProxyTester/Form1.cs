using MihaZupan;
using Org.Mentalis.Network.ProxySocket;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ProxyTester
{
    public partial class Form1 : Form
    {
        //https://github.com/extremecodetv/SocksSharp //SocksSharp
        //https://github.com/postworthy/SocksWebProxy //SocksWebProxy

        private readonly string nl = Environment.NewLine;
        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click_001;
        }

        private void CheckSsl()
        {
            string host = "ru.stackoverflow.com";

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(host, 443);
            SslStream tls = new SslStream(new NetworkStream(socket));
            tls.AuthenticateAsClient(host);
            
            var request = $"GET / HTTP/1.0{nl}Host: {host}{nl}{nl}";

            tls.Write(Encoding.ASCII.GetBytes(request));

            using (var sr = new StreamReader(tls, Encoding.UTF8))
            {
                string response = sr.ReadToEnd();
                Console.WriteLine(response);
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            //https://github.com/MihaZupan/HttpToSocks5Proxy
            textBox1.Text = string.Empty;
            foreach (var proxyAddress in tbProxies.Lines)
            {
                try
                {
                    var ipport = proxyAddress.Split(':');
                    var ipString = ipport[0];
                    var port = int.Parse(ipport[1]);

                    var proxy = new HttpToSocks5Proxy(ipString, port);

                    var handler = new HttpClientHandler { Proxy = proxy };
                    HttpClient httpClient = new HttpClient(handler, true);
                    proxy.ResolveHostnamesLocally = true;

                    var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://check-host.net/ip"));
                    result.EnsureSuccessStatusCode();

                    textBox1.Text += await result.Content.ReadAsStringAsync();
                    //string page = client.Get("https://httpbin.org/get");
                    //string page = client.Get("https://httpbin.org/ip");
                    //http://check-host.net/ip
                }
                catch (Exception exception)
                {

                    textBox1.Text = $"{exception}";
                }
            }
        }


        private void button1_Click_001(object sender, EventArgs e)
        {
            //https://github.com/poma/ProxySocket
            textBox1.Text = string.Empty;
            foreach (var proxyAddress in tbProxies.Lines)
            {
                try
                {
                    var ipport = proxyAddress.Split(':');
                    var ipString = ipport[0];
                    var port = int.Parse(ipport[1]);

                    // create a new ProxySocket
                    ProxySocket socket = new ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // set the proxy settings
                    socket.ProxyEndPoint = new IPEndPoint(IPAddress.Parse(ipString), port);
                    //s.ProxyUser = "username";
                    //s.ProxyPass = "password";
                    socket.SendTimeout = 2000;
                    socket.ProxyType = ProxyTypes.Https;    // if you set this to ProxyTypes.None, 
                                                             // the ProxySocket will act as a normal Socket
                                                             // connect to the remote server
                                                             // (note that the proxy server will resolve the domain name for us)
                    
                    var uri = new Uri("https://httpbin.org/ip");
                    var host = uri.Host;
                    var path = uri.AbsolutePath;
                    socket.Connect(host, 443);
                    //SslStream tls = new SslStream(new NetworkStream(socket));
                    //tls.AuthenticateAsClient(host);
                    var request = $"GET {path} HTTP/1.0{nl}Host: {host}{nl}{nl}";

                    //tls.Write(Encoding.ASCII.GetBytes(request));
                    //using (var sr = new StreamReader(tls, Encoding.UTF8))
                    //{
                    //    string response = sr.ReadToEnd();
                    //    textBox1.Text += response;
                    //}
                    //return;

                    // send an HTTP request
                    socket.Send(Encoding.ASCII.GetBytes(request));
                    // read the HTTP reply
                    int recv = 0;
                    byte[] buffer = new byte[1024];
                    recv = socket.Receive(buffer);
                    StringBuilder stringBuilder = new StringBuilder();
                    while (recv > 0)
                    {
                        stringBuilder.Append(Encoding.ASCII.GetString(buffer, 0, recv));
                        recv = socket.Receive(buffer);
                    }
                    textBox1.Text += stringBuilder.ToString();
                    //string page = client.Get("https://httpbin.org/get");
                    //http://check-host.net/ip
                }
                catch (Exception exception)
                {

                    textBox1.Text = $"{exception}";
                }
            }
        }
    }
}
