using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApiUtils.ApiAddresses;
using WebApiUtils.Entities;

namespace WebApiUtils
{
    public class DarkHttpClient : IDisposable
    {
        public static HttpClientHandler CreateHandler() => new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        private HttpClient client;

        public DarkHttpClient()
        {
            var httpClientHelper = CreateHandler();
            client = new HttpClient(httpClientHelper);
            DarkAuth.SetHttpAuth(client);
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

            public DarkHttpRequestMessage SetContent<T>(T content)
            {
                message.Content = JsonContent.Create(content);
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

    public static class DarkHttpClientExt
    {
        public static DResponse<T[]>? GetAllFrom<T>(this DarkHttpClient client, BaseApiMethods api)
        {
            var result = client.CreateRequest()
                .SetMethodGet()
                .SetUri(api.GetAll)
                .SendAsync().Result;
            var content = result.Content;
            return content.ReadFromJsonAsync(typeof(DResponse<T[]>)).Result as DResponse<T[]>;
        }

        public static DResponse<T>? GetByIdFrom<T>(this DarkHttpClient client, BaseApiMethods api, int id)
            where T : class
        {
            var result = client.CreateRequest()
                .SetMethodGet()
                .SetUri($"{api.GetById}?id={id}")
                .SendAsync().Result;
            var content = result.Content;
            return content.ReadFromJsonAsync(typeof(DResponse<T>)).Result as DResponse<T>;
        }

        public static DResponse<T>? AddFrom<T>(this DarkHttpClient client, BaseApiMethods api, T item)
            where T : class
        {
            var result = client.CreateRequest()
                .SetMethodPost()
                .SetUri(api.Add)
                .SetContent(item)
                .SendAsync().Result;
            var content = result.Content;
            return content.ReadFromJsonAsync(typeof(DResponse<T>)).Result as DResponse<T>;
        }
    }
}
