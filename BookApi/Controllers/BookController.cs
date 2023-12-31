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
                if (item.AuthorId is not null)
                {
                    var response = client.CreateRequest()
                        .SetMethodGet()
                        .SetUri($"{ApiDictionary.AuthorApi.GetById}?id={item.AuthorId}")
                        .SendAsync().Result
                        .Content.ReadFromJsonAsync(typeof(DResponse<DEntityIdName>)).Result as DResponse<DEntityIdName>;

                    if (response?.Data is null)
                    {
                        return DResponse<object>.Error($"Author with id {item.AuthorId} not exists");
                    }
                }

                if (item.SeriesId is not null)
                {
                    var response = client.CreateRequest()
                        .SetMethodGet()
                        .SetUri($"{ApiDictionary.BookSeriesApi.GetById}?id={item.SeriesId}")
                        .SendAsync().Result
                        .Content.ReadFromJsonAsync(typeof(DResponse<DEntityIdName>)).Result as DResponse<DEntityIdName>;

                    if (response?.Data is null)
                    {
                        return DResponse<object>.Error($"Book series with id {item.SeriesId} not exists");
                    }
                }

                return base.Add(item);
            }
        }
    }
}
