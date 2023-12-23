using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DarkLibrary.Controllers
{
    [Route("/client")]
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new LibraryDbContext())
            {
                var items = db.Branches.ToArray();
                return View("Index", items);
            }
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View("Create", null);
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

            using (var db = new LibraryDbContext())
            {
                if (db.Clients.Any(item => item.Name == name))
                {
                    return View("Create", $"Error: Client \"{name}\" already exists");
                }

                var item = new DClient { Name = name };
                db.Clients.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
