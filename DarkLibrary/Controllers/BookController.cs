using DarkLibrary.Models;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DarkLibrary.Controllers
{
    [Route("/book")]
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new LibraryDbContext())
            {
                var items = db.Books
                    .Include(book => book.Author)
                    .Include(book => book.Series)
                    .ToArray();
                return View("Index", items);
            }
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            using (var db = new LibraryDbContext())
            {
                var model = PrepareCreateBookModel(db);
                return View("Create", model);
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePost()
        {
            var name = Request.Form["name"].ToString().Trim();
            var authorIdInput = Request.Form["authorId"];
            var seriesIdInput = Request.Form["seriesId"];

            using (var db = new LibraryDbContext())
            {
                if (name is null || name.Length < 1)
                {
                    var model = PrepareCreateBookModel(db);
                    model.ErrorText = "Error: Name can not be empty";
                    return View("Create", model);
                }

                if (db.Books.Any(item => item.Name == name))
                {
                    var model = PrepareCreateBookModel(db);
                    model.ErrorText = $"Error: Book series \"{name}\" already exists";
                    return View("Create", model);
                }

                var item = new DBook
                {
                    Name = name
                };

                if (int.TryParse(authorIdInput, out int authorId))
                {
                    var author = db.Authors.FirstOrDefault(author => author.Id == authorId);
                    if (author is null)
                    {
                        var model = PrepareCreateBookModel(db);
                        model.ErrorText = $"Error: Incorrect author id";
                        return View("Create", model);
                    }
                    item.Author = author;
                }

                if (int.TryParse(seriesIdInput, out int seriesId))
                {
                    var series = db.BookSeries.FirstOrDefault(series => series.Id == seriesId);
                    if (series is null)
                    {
                        var model = PrepareCreateBookModel(db);
                        model.ErrorText = $"Error: Incorrect series id";
                        return View("Create", model);
                    }
                    item.Series = series;
                }

                db.Books.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        private CreateBookModel PrepareCreateBookModel(LibraryDbContext db)
        {
            return new CreateBookModel
            {
                Authors = db.Authors.ToArray(),
                Series = db.BookSeries.ToArray(),
            };
        }
    }
}
