using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
