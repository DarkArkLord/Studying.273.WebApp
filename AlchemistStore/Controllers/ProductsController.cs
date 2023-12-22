using DataLayer.Entities;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Linq;

namespace AlchemistStore.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info(int id)
        {
            using (var db = new StoreDbContext())
            {
                var product = db.Products.FirstOrDefault(x => x.Id == id);
                return View("Info", product);
            }
        }
    }
}
