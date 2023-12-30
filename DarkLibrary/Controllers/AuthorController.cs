using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DarkLibrary.Controllers
{
    [Route("/authors")]
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new LibraryDbContext())
            {
                var items = db.Authors.ToArray();
                return ReturnIndexWithList("Author menu", items);
            }
        }

        private IActionResult ReturnIndexWithList(string title, IEnumerable<DEntityIdName> items)
        {
            ViewData["Title"] = title;
            ViewData["Controller"] = "Author";
            return View("Views/IdNameViews/Index.cshtml", items);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return ReturnCreateWithErrorText("Create Author", null);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePost()
        {
            var name = Request.Form["name"].ToString().Trim();
            if (name is null || name.Length < 1)
            {
                return ReturnCreateWithErrorText("Create Author", "Error: Name can not be empty");
            }

            using (var db = new LibraryDbContext())
            {
                if (db.Authors.Any(author => author.Name == name))
                {
                    return ReturnCreateWithErrorText("Create Author", $"Error: Author \"{name}\" already exists", name);
                }

                var author = new DAuthor { Name = name };
                db.Authors.Add(author);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        private IActionResult ReturnCreateWithErrorText(string title, string? errorText, string name = "")
        {
            ViewData["Title"] = title;
            ViewData["ErrorText"] = errorText;
            ViewData["EntityName"] = name;
            return View("Views/IdNameViews/Create.cshtml");
        }
    }
}
