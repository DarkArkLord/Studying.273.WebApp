using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AuthorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountController : ControllerBase
    {
        [HttpGet()]
        public int Get()
        {
            //using (var db = new AuthorDbContext())
            //{
            //    return db.Authors.Count();
            //}
            return 6;
        }
    }
}
