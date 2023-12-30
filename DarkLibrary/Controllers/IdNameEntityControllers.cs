using Azure.Core;
using DataLayer.Entities;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUtils;

namespace DarkLibrary.Controllers
{
    public abstract class BaseIdNameEntityControllers<TDbContext, TEntity> : Controller
        where TDbContext : DbContext
        where TEntity : DEntityIdName, new()
    {
        protected abstract string IndexTitle { get; }
        protected abstract string CreateTitle { get; }
        protected abstract string ControllerName { get; }

        protected IActionResult ReturnIndexWithList(IEnumerable<DEntityIdName> items)
        {
            ViewData["Title"] = IndexTitle;
            ViewData["Controller"] = ControllerName;
            return View("Views/IdNameViews/Index.cshtml", items);
        }

        protected IActionResult ReturnCreateWithErrorText(string? errorText, string name = "")
        {
            ViewData["Title"] = CreateTitle;
            ViewData["ErrorText"] = errorText;
            ViewData["EntityName"] = name;
            return View("Views/IdNameViews/Create.cshtml");
        }

        protected abstract TDbContext CreateDbContext();
        protected abstract DbSet<TEntity> GetDbSet(TDbContext context);

        public IActionResult Index()
        {
            using (var db = CreateDbContext())
            {
                var items = GetDbSet(db).ToArray();
                return ReturnIndexWithList(items);
            }
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return ReturnCreateWithErrorText(null);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePost()
        {
            var name = Request.Form["name"].ToString().Trim();
            if (name is null || name.Length < 1)
            {
                return ReturnCreateWithErrorText("Error: Name can not be empty");
            }

            using (var db = CreateDbContext())
            {
                if (GetDbSet(db).Any(item => item.Name == name))
                {
                    return ReturnCreateWithErrorText($"Error: {ControllerName} \"{name}\" already exists", name);
                }

                var item = new DEntityIdName { Name = name };
                TEntity entity = DarkConverter.Convert<DEntityIdName, TEntity>(item)!;
                GetDbSet(db).Add(entity);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }


    [Route("/authors")]
    public class AuthorController : BaseIdNameEntityControllers<LibraryDbContext, DAuthor>
    {
        protected override string IndexTitle => "Author menu";
        protected override string CreateTitle => "Create Author";
        protected override string ControllerName => "Author";

        protected override LibraryDbContext CreateDbContext() => new LibraryDbContext();
        protected override DbSet<DAuthor> GetDbSet(LibraryDbContext context) => context.Authors;
    }
}
