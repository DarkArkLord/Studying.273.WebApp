using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiUtils
{
    public class DarkHttpClient : IDisposable
    {
        private HttpClient client;

        public DarkHttpClient()
        {
            var httpClientHelper = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            client = new HttpClient(httpClientHelper);
        }

        public DarkHttpRequestMessage CreateRequest()
        {
            return new DarkHttpRequestMessage(client);
        }

        public void Dispose()
        {
            client.Dispose();
        }

        #region HttpRequestMessage
        public class DarkHttpRequestMessage
        {
            private HttpClient client;
            private HttpRequestMessage message;

            public DarkHttpRequestMessage(HttpClient client)
            {
                this.client = client;
                message = new HttpRequestMessage();
            }

            public DarkHttpRequestMessage SetMethod(HttpMethod method)
            {
                message.Method = method;
                return this;
            }

            public DarkHttpRequestMessage SetMethodGet()
            {
                return SetMethod(HttpMethod.Get);
            }

            public DarkHttpRequestMessage SetMethodPost()
            {
                return SetMethod(HttpMethod.Post);
            }

            public DarkHttpRequestMessage SetUri(string uri)
            {
                message.RequestUri = new Uri(uri);
                return this;
            }

            public HttpResponseMessage Send()
            {
                return client.Send(message);
            }

            public async Task<HttpResponseMessage> SendAsync()
            {
                return await client.SendAsync(message);
            }
        }
        #endregion
    }
}
