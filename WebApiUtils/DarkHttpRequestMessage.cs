using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiUtils
{
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
}
