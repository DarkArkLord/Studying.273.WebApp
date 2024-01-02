using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Json;
using WebApiUtils;
using WebApiUtils.ApiAddresses;
using WebApiUtils.BaseApi;
using WebApiUtils.Entities;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : BaseApiWithNameController<DBook, BaseWithNameRepository<DBook>>
    {
        protected override string connectionString => Environment.GetEnvironmentVariable("ConnectionString")!;
        protected override BaseWithNameRepository<DBook> repository => new BaseWithNameRepository<DBook>(connectionString);

        public override DResponse<object> Add(DBook item)
        {
            using (var client = new DarkHttpClient())
            {
                var authorResponce = CheckAuthor(item.AuthorId, client);
                if (authorResponce is not null) return authorResponce;

                var seriesResponce = CheckSeries(item.SeriesId, client);
                if (seriesResponce is not null) return seriesResponce;

                return base.Add(item);
            }
        }

        public override DResponse<object> Update(DBook item)
        {
            using (var client = new DarkHttpClient())
            {
                var authorResponce = CheckAuthor(item.AuthorId, client);
                if (authorResponce is not null) return authorResponce;

                var seriesResponce = CheckAuthor(item.SeriesId, client);
                if (seriesResponce is not null) return seriesResponce;

                return base.Update(item);
            }
        }

        private DResponse<object>? CheckAuthor(int? authorId, DarkHttpClient httpClient)
        {
            if (authorId is not null)
            {
                var response = httpClient.CreateRequest()
                    .SetMethodGet()
                    .SetUri($"{ApiDictionary.AuthorApi.GetById}?id={authorId}")
                    .SendAsync().Result
                    .Content.ReadFromJsonAsync(typeof(DResponse<DEntityIdName>)).Result as DResponse<DEntityIdName>;

                if (response?.Data is null)
                {
                    return DResponse<object>.Error($"Author with id {authorId} not exists");
                }
            }

            return null;
        }

        private DResponse<object>? CheckSeries(int? seriesId, DarkHttpClient httpClient)
        {
            if (seriesId is not null)
            {
                var response = httpClient.CreateRequest()
                    .SetMethodGet()
                    .SetUri($"{ApiDictionary.BookSeriesApi.GetById}?id={seriesId}")
                    .SendAsync().Result
                    .Content.ReadFromJsonAsync(typeof(DResponse<DEntityIdName>)).Result as DResponse<DEntityIdName>;

                if (response?.Data is null)
                {
                    return DResponse<object>.Error($"Book series with id {seriesId} not exists");
                }
            }

            return null;
        }
    }
}
