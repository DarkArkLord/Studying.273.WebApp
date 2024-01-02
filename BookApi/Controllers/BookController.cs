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

        public override DResponse<DBook> Add(DBook item)
        {
            using (var client = new DarkHttpClient())
            {
                var authorResponce = CheckOtherObjectExists(item.AuthorId, client, ApiDictionary.AuthorApi.GetById, "Author", item);
                if (authorResponce is not null) return authorResponce;

                var seriesResponce = CheckOtherObjectExists(item.SeriesId, client, ApiDictionary.BookSeriesApi.GetById, "Book series", item);
                if (seriesResponce is not null) return seriesResponce;

                return base.Add(item);
            }
        }

        public override DResponse<DBook> Update(DBook item)
        {
            using (var client = new DarkHttpClient())
            {
                var authorResponce = CheckOtherObjectExists(item.AuthorId, client, ApiDictionary.AuthorApi.GetById, "Author", item);
                if (authorResponce is not null) return authorResponce;

                var seriesResponce = CheckOtherObjectExists(item.SeriesId, client, ApiDictionary.BookSeriesApi.GetById, "Book series", item);
                if (seriesResponce is not null) return seriesResponce;

                return base.Update(item);
            }
        }
    }
}
