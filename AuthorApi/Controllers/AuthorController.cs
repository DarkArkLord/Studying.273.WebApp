using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiUtils.BaseApi;

namespace AuthorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : BaseApiWithNameController
    {
        protected override string connectionString => Environment.GetEnvironmentVariable("ConnectionString")!;
        protected override BaseWithNameRepository repository => new BaseWithNameRepository(connectionString);
    }
}
