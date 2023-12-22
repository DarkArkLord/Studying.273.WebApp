using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Store.ElementsApi.Controllers
{
    [ApiController]
    [Route("store/api/[controller]")]
    public class ElementsController : Controller
    {
        [HttpGet]
        public IEnumerable<DBranch> Index()
        {
            using (var db = new StoreDbContext())
            {
                var el1 = new DElement { Name = "e1" };
                var el2 = new DElement { Name = "e2" };

                db.Elements.Add(el1);
                db.Elements.Add(el2);
                db.SaveChanges();

                var prd = new DProduct
                {
                    Name = "p1",
                    Price = 10,
                    Elements = db.Elements.Select(elem => new DElementPurityProduct { Element = elem, Purity = 0.5 }).ToList()
                };


                db.Products.Add(prd);
                db.SaveChanges();

                var branch = new DBranch
                {
                    Products = db.Products.Select(p => new DProductWeightBranch { Product = p, Weight = 5 }).ToList()
                };

                db.Branches.Add(branch);
                db.SaveChanges();

                return db.Branches.ToArray();
            }
        }
    }
}
