using Microsoft.AspNetCore.Mvc;
using WebApiUtils;
using WebApiUtils.ApiAddresses;
using WebApiUtils.Entities;

namespace DarkLibrary.Controllers
{
    public abstract class BaseIdNameEntityControllers : Controller
    {
        protected abstract string IndexTitle { get; }
        protected abstract string CreateTitle { get; }
        protected abstract string ControllerName { get; }
        protected abstract NamedApiMethods ApiAddresses { get; }

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

        protected IActionResult ReturnError(string? errorText)
        {
            ViewData["ErrorText"] = errorText;
            return View("Views/Shared/ErrorView.cshtml");
        }

        public IActionResult Index()
        {
            using (var client = new DarkHttpClient())
            {
                var items = client.GetAllFrom<DEntityIdName>(ApiAddresses);

                if (items is null) return ReturnError("Request error");
                if (!items.IsSuccess) return ReturnError(items.Message);

                return ReturnIndexWithList(items.Data!);
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

            using (var client = new DarkHttpClient())
            {
                var item = new DEntityIdName { Name = name };
                var response = client.AddFrom(ApiAddresses, item);

                if (response is null) return ReturnError("Request error");
                if (!response.IsSuccess) return ReturnCreateWithErrorText(response.Message, name);

                return RedirectToAction("Index");
            }
        }
    }


    [Route("/authors")]
    public class AuthorController : BaseIdNameEntityControllers
    {
        protected override string IndexTitle => "Author menu";
        protected override string CreateTitle => "Create Author";
        protected override string ControllerName => "Author";
        protected override NamedApiMethods ApiAddresses => ApiDictionary.AuthorApi;
    }


    [Route("/series")]
    public class BookSeriesController : BaseIdNameEntityControllers
    {
        protected override string IndexTitle => "Series menu";
        protected override string CreateTitle => "Create Book Series";
        protected override string ControllerName => "BookSeries";
        protected override NamedApiMethods ApiAddresses => ApiDictionary.BookSeriesApi;
    }


    [Route("/branch")]
    public class BranchController : BaseIdNameEntityControllers
    {
        protected override string IndexTitle => "Branch menu";
        protected override string CreateTitle => "Create Branch";
        protected override string ControllerName => "Branch";
        protected override NamedApiMethods ApiAddresses => ApiDictionary.BranchApi;
    }


    [Route("/librarian")]
    public class LibrarianController : BaseIdNameEntityControllers
    {
        protected override string IndexTitle => "Librarian menu";
        protected override string CreateTitle => "Create Librarian";
        protected override string ControllerName => "Librarian";
        protected override NamedApiMethods ApiAddresses => ApiDictionary.LibratianApi;
    }


    [Route("/client")]
    public class ClientController : BaseIdNameEntityControllers
    {
        protected override string IndexTitle => "Client menu";
        protected override string CreateTitle => "Create Client";
        protected override string ControllerName => "Client";
        protected override NamedApiMethods ApiAddresses => ApiDictionary.ClientApi;
    }
}
