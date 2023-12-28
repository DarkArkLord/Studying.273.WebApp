using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AuthorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllController : ControllerBase
    {
        [HttpGet()]
        public IEnumerable<DAuthor> Get()
        {
            using (var db = new AuthorDbContext())
            {
                return db.Authors.ToArray();
            }
        }
    }
}
