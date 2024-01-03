using DarkLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUtils;
using WebApiUtils.ApiAddresses;
using WebApiUtils.Entities;

namespace DarkLibrary.Controllers
{
    [Authorize]
    [Route("/book")]
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            using (var client = new DarkHttpClient())
            {
                try
                {
                    var books = client.GetAllFrom<DBook>(ApiDictionary.BookApi);
                    var items = books.Data.Select(item => MapBookToLinks(item, client)).ToArray();
                    return View("Index", items);
                }
                catch (Exception ex)
                {
                    return ReturnError(ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            using (var db = new DarkHttpClient())
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
            var authorIdInput = Request.Form["authorId"].ToString();
            var seriesIdInput = Request.Form["seriesId"].ToString();

            using (var client = new DarkHttpClient())
            {
                var item = new DBook
                {
                    Name = name
                };

                if (authorIdInput is not null && authorIdInput.Length > 0)
                {
                    if (int.TryParse(authorIdInput, out int authorId))
                    {
                        item.AuthorId = authorId;
                    }
                    else
                    {
                        var model = PrepareCreateBookModel(client);
                        model.ErrorText = $"Error: Incorrect author id";
                        return View("Create", model);
                    }
                }

                if (seriesIdInput is not null && seriesIdInput.Length > 0)
                {
                    if (int.TryParse(seriesIdInput, out int seriesId))
                    {
                        item.SeriesId = seriesId;
                    }
                    else
                    {
                        var model = PrepareCreateBookModel(client);
                        model.ErrorText = $"Error: Incorrect series id";
                        return View("Create", model);
                    }
                }

                var response = client.AddFrom(ApiDictionary.BookApi, item);

                if (response is null) return ReturnError("Request error");
                if (!response.IsSuccess)
                {
                    var model = PrepareCreateBookModel(client);
                    model.ErrorText = response.Message;
                    return View("Create", model);
                }

                return RedirectToAction("Index");
            }
        }

        private IActionResult ReturnError(string? errorText)
        {
            ViewData["ErrorText"] = errorText;
            return View("Views/Shared/ErrorView.cshtml");
        }

        private DBookLinked MapBookToLinks(DBook item, DarkHttpClient client)
        {
            var result = new DBookLinked
            {
                Id = item.Id,
                Name = item.Name,
                Author = null,
                Series = null,
            };

            if (item.AuthorId is not null)
            {
                result.Author = GetBookPart(client, ApiDictionary.AuthorApi, item.Id, (int)item.AuthorId, "Author");
            }

            if (item.SeriesId is not null)
            {
                result.Series = GetBookPart(client, ApiDictionary.BookSeriesApi, item.Id, (int)item.SeriesId, "Series");
            }

            return result;
        }

        private DEntityIdName GetBookPart(DarkHttpClient client, BaseApiMethods api, int itemId, int partId, string name)
        {
            var part = client.GetByIdFrom<DEntityIdName>(api, partId);

            if (part is null) throw new Exception($"Request error for book (id: {itemId}) {name} (id: {partId})");
            if (!part.IsSuccess) throw new Exception($"Request error for book (id: {itemId}) {name} (id: {partId})");
            if (part.Data is null) throw new Exception($"{name} (id: {partId}) for book (id: {itemId}) not exists");

            return part.Data;
        }

        private CreateBookModel PrepareCreateBookModel(DarkHttpClient client)
        {
            var authors = client.GetAllFrom<DEntityIdName>(ApiDictionary.AuthorApi);
            var series = client.GetAllFrom<DEntityIdName>(ApiDictionary.BookSeriesApi);

            return new CreateBookModel
            {
                Authors = authors.Data,
                Series = series.Data,
            };
        }
    }
}
