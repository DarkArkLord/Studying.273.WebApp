using System;
using System.Net.Http;

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
    }
}
