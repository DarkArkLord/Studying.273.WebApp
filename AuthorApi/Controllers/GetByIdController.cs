using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuthorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetByIdController : ControllerBase
    {
        [HttpGet()]
        public DAuthor? Get(int id)
        {
            //using (var db = new AuthorDbContext())
            //{
            //    var item = db.Authors.FirstOrDefault(item => item.Id == id);
            //    return item;
            //}
            return null;
        }
    }
}
