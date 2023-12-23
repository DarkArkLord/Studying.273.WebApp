using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DarkLibrary.Controllers
{
    [Route("/series")]
    public class BookSeriesController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new LibraryDbContext())
            {
                var items = db.BookSeries.ToArray();
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
                if (db.BookSeries.Any(item => item.Name == name))
                {
                    return View("Create", $"Error: Book series \"{name}\" already exists");
                }

                var item = new DBookSeries { Name = name };
                db.BookSeries.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
