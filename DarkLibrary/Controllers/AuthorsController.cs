using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DarkLibrary.Controllers
{
    [Route("/authors")]
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new LibraryDbContext())
            {
                var items = db.Authors.ToArray();
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
                if (db.Authors.Any(author => author.Name == name))
                {
                    return View("Create", $"Error: Author \"{name}\" already exists");
                }

                var author = new DAuthor { Name = name };
                db.Authors.Add(author);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
