using Microsoft.AspNetCore.Mvc;
using System;
using WebApiUtils.BaseApi;

namespace BookSeriesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookSeriesController : BaseApiWithNameController
    {
        protected override string connectionString => Environment.GetEnvironmentVariable("ConnectionString")!;
        protected override BaseWithNameRepository repository => new BaseWithNameRepository(connectionString);
    }
}
