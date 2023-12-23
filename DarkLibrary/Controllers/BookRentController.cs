using DarkLibrary.Models;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DarkLibrary.Controllers
{
    [Route("/rent")]
    public class BookRentController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new LibraryDbContext())
            {
                var items = db.BookRents
                    .Include(rent => rent.Book)
                    .Include(rent => rent.Client)
                    .Include(rent => rent.Librarian)
                    .Include(rent => rent.Branch)
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
                var model = PrepareCreateRentModel(db);
                return View("Create", model);
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePost()
        {
            var bookIdInput = Request.Form["bookId"];
            var clientIdInput = Request.Form["clientId"];
            var librarianIdInput = Request.Form["librarianId"];
            var branchIdInput = Request.Form["branchId"];

            var rentDateInput = Request.Form["rentDate"];
            var rentDaysInput = Request.Form["rentDays"];

            using (var db = new LibraryDbContext())
            {
                var item = new DBookRent();

                if (DateTime.TryParse(rentDateInput, out DateTime rentDate))
                {
                    item.RentDate = rentDate;
                }
                else
                {
                    return CreateWithError(db, "Error: Incorrect rent date");
                }

                if (int.TryParse(rentDaysInput, out int rentDays))
                {
                    if (rentDays < 1)
                    {
                        return CreateWithError(db, "Error: Rent days must be positive");
                    }

                    item.RentDays = rentDays;
                }
                else
                {
                    return CreateWithError(db, "Error: Incorrect rent days");
                }

                if (int.TryParse(bookIdInput, out int bookId))
                {
                    var book = db.Books.FirstOrDefault(book => book.Id == bookId);
                    if (book is null)
                    {
                        return CreateWithError(db, $"Error: No book with \"{bookId}\" id");
                    }
                    item.Book = book;
                }
                else
                {
                    return CreateWithError(db, "Error: Incorrect book id");
                }

                if (int.TryParse(clientIdInput, out int clientId))
                {
                    var client = db.Clients.FirstOrDefault(client => client.Id == clientId);
                    if (client is null)
                    {
                        return CreateWithError(db, $"Error: No client with \"{clientId}\" id");
                    }
                    item.Client = client;
                }
                else
                {
                    return CreateWithError(db, "Error: Incorrect client id");
                }

                if (int.TryParse(librarianIdInput, out int librarianId))
                {
                    var librarian = db.Librarians.FirstOrDefault(librarian => librarian.Id == librarianId);
                    if (librarian is null)
                    {
                        return CreateWithError(db, $"Error: No librarian with \"{librarianId}\" id");
                    }
                    item.Librarian = librarian;
                }
                else
                {
                    return CreateWithError(db, "Error: Incorrect librarian id");
                }

                if (int.TryParse(branchIdInput, out int branchId))
                {
                    var branch = db.Branches.FirstOrDefault(branch => branch.Id == branchId);
                    if (branch is null)
                    {
                        return CreateWithError(db, $"Error: No branch with \"{branchId}\" id");
                    }
                    item.Branch = branch;
                }
                else
                {
                    return CreateWithError(db, "Error: Incorrect branch id");
                }

                db.BookRents.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        private CreateRentModel PrepareCreateRentModel(LibraryDbContext db)
        {
            return new CreateRentModel
            {
                Books = db.Books.ToArray(),
                Clients = db.Clients.ToArray(),
                Librarians = db.Librarians.ToArray(),
                Branches = db.Branches.ToArray(),
            };
        }

        private IActionResult CreateWithError(LibraryDbContext db, string message)
        {
            var model = PrepareCreateRentModel(db);
            model.ErrorText = message;
            return View("Create", model);
        }
    }
}
