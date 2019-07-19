using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Service
{
    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler()
            : this(new HttpClientHandler())
        {
        }

        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Debug.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Debug.WriteLine(await request.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false));
            }
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            //using (var stream = await response.Content.ReadAsStreamAsync())
            //{
            //    //stream.Position = 0;
            //    using (var reader = new StreamReader(stream, Encoding.UTF8))
            //    {
            //        Debug.WriteLine(reader.ReadToEnd());
            //    }
            //}

            return response;
        }
    }
}