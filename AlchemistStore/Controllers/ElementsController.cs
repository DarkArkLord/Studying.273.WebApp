using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AlchemistStore.Controllers
{
    [Route("/elements")]
    public class ElementsController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new StoreDbContext())
            {
                var elements = db.Elements.ToArray();
                return View(elements);
            }
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View(null);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePost()
        {
            var name = Request.Form["name"].ToString().Trim();

            if (name is null || name.Length < 1)
            {
                return View("Create", "Error: Name can not be empty");
            }

            using (var db = new StoreDbContext())
            {
                if (db.Elements.Any(elem => elem.Name == name))
                {
                    return View("Create", $"Error: Element \"{name}\" already exists");
                }

                var element = new DElement { Name = name };
                db.Elements.Add(element);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
