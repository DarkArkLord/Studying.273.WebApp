using DarkLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiUtils;
using WebApiUtils.ApiAddresses;
using WebApiUtils.Entities;

namespace DarkLibrary.Controllers
{
    [Route("/rent")]
    public class BookRentController : Controller
    {
        public IActionResult Index()
        {
            using (var client = new DarkHttpClient())
            {
                try
                {
                    var rents = client.GetAllFrom<DBookRent>(ApiDictionary.BookRentApi);
                    var items = rents.Data.Select(item => MapRentToLinks(item, client)).ToArray();
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
            using (var client = new DarkHttpClient())
            {
                var model = PrepareCreateRentModel(client);
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

            using (var client = new DarkHttpClient())
            {
                var item = new DBookRent();

                if (DateTime.TryParse(rentDateInput, out DateTime rentDate))
                {
                    item.OpenDate = rentDate;
                }
                else
                {
                    return CreateWithError(client, "Error: Incorrect open rent date");
                }

                if (int.TryParse(rentDaysInput, out int rentDays))
                {
                    item.RentDays = rentDays;
                }
                else
                {
                    return CreateWithError(client, "Error: Incorrect rent days");
                }

                if (int.TryParse(bookIdInput, out int bookId))
                {
                    item.BookId = bookId;
                }
                else
                {
                    return CreateWithError(client, "Error: Incorrect book id");
                }

                if (int.TryParse(clientIdInput, out int clientId))
                {
                    item.ClientId = clientId;
                }
                else
                {
                    return CreateWithError(client, "Error: Incorrect client id");
                }

                if (int.TryParse(librarianIdInput, out int librarianId))
                {
                    item.LibrarianId = librarianId;
                }
                else
                {
                    return CreateWithError(client, "Error: Incorrect librarian id");
                }

                if (int.TryParse(branchIdInput, out int branchId))
                {
                    item.BranchId = branchId;
                }
                else
                {
                    return CreateWithError(client, "Error: Incorrect branch id");
                }

                var response = client.AddFrom(ApiDictionary.BookRentApi, item);
                if (response is null) return ReturnError("Request error");
                if (!response.IsSuccess)
                {
                    return CreateWithError(client, response.Message);
                }

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("close/{id}")]
        public IActionResult Close(int id)
        {
            using (var client = new DarkHttpClient())
            {
                try
                {
                    var model = PrepareCloseRentModel(client, id);
                    if (model.Rent is null)
                    {
                        model.ErrorText = "Error: Incorrect rent id";
                    }
                    return View("Close", model);
                }
                catch (Exception ex)
                {
                    return ReturnError(ex.Message);
                }
            }
        }

        [HttpPost]
        [Route("calculate/{id}")]
        public IActionResult CloseCalculate(int id)
        {
            using (var client = new DarkHttpClient())
            {
                try
                {
                    var model = PrepareCloseRentModel(client, id);
                    if (model.Rent is null)
                    {
                        model.ErrorText = "Error: Incorrect rent id";
                        return View("Close", model);
                    }

                    var rentEndDateInput = Request.Form["rentEndDate"];
                    var penaltyInput = Request.Form["penalty"];

                    if (DateTime.TryParse(rentEndDateInput, out DateTime rentEndDate))
                    {
                        model.EndDate = rentEndDate;
                    }
                    else
                    {
                        model.ErrorText = "Error: Incorrect rent end date";
                        return View("Close", model);
                    }

                    if (int.TryParse(penaltyInput, out int penaltyByDay))
                    {
                        model.PenaltyByDay = penaltyByDay;
                    }
                    else
                    {
                        model.ErrorText = "Error: Incorrect penalty";
                        return View("Close", model);
                    }

                    var response = client.CreateRequest()
                        .SetMethodGet()
                        .SetUri($"{ApiDictionary.BookRentApi.Calculate}?rentId={id}&closeDate={rentEndDate}&penaltyByDay={penaltyByDay}")
                        .SendAsync().Result.Content
                        .ReadFromJsonAsync(typeof(DResponse<DBookRent>)).Result as DResponse<DBookRent>;

                    if (response is null) throw new Exception($"Request error for rent (id: {id}) calculation");
                    else if (!response.IsSuccess) model.ErrorText = response.Message;
                    else if (response.Data is null) throw new Exception($"Request error for rent (id: {id}) calculation");

                    model.Rent = MapRentToLinks(response.Data, client);
                    model.EndDate = model.Rent.CloseDate;
                    model.PenaltyByDay = penaltyByDay;
                    model.TotalPenalty = model.Rent.Penalty;

                    return View("Close", model);
                }
                catch (Exception ex)
                {
                    return ReturnError(ex.Message);
                }
            }
        }

        [HttpPost]
        [Route("close/{id}")]
        public IActionResult ClosePost(int id)
        {
            using (var client = new DarkHttpClient())
            {
                var model = PrepareCloseRentModel(client, id);
                if (model.Rent is null)
                {
                    model.ErrorText = "Error: Incorrect rent id";
                    return View("Close", model);
                }

                var rentEndDateInput = Request.Form["rentEndDate"];
                var penaltyInput = Request.Form["penalty"];

                if (!DateTime.TryParse(rentEndDateInput, out DateTime rentEndDate))
                {
                    model.ErrorText = "Error: Incorrect rent end date";
                    return View("Close", model);
                }

                if (!int.TryParse(penaltyInput, out int penalty))
                {
                    model.ErrorText = "Error: Incorrect penalty";
                    return View("Close", model);
                }

                var response = client.CreateRequest()
                    .SetMethodPost()
                    .SetUri($"{ApiDictionary.BookRentApi.Close}?rentId={id}&closeDate={rentEndDate}&penalty={penalty}")
                    .SendAsync().Result.Content
                    .ReadFromJsonAsync(typeof(DResponse<DBookRent>)).Result as DResponse<DBookRent>;

                if (response is null)
                {
                    model.ErrorText = $"Request error for rent (id: {id}) calculation";
                    return View("Close", model);
                }
                if (!response.IsSuccess)
                {
                    model.ErrorText = response.Message;
                    return View("Close", model);
                }
                if (response.Data is null)
                {
                    model.ErrorText = $"Request error for rent (id: {id}) calculation";
                    return View("Close", model);
                }

                return RedirectToAction("Index");
            }
        }

        private IActionResult ReturnError(string? errorText)
        {
            ViewData["ErrorText"] = errorText;
            return View("Views/Shared/ErrorView.cshtml");
        }

        private DBookRentLinked MapRentToLinks(DBookRent item, DarkHttpClient client)
        {
            if (item is null) return null;

            var result = new DBookRentLinked
            {
                Id = item.Id,
                Book = null,
                Client = null,
                Librarian = null,
                Branch = null,
                OpenDate = item.OpenDate,
                RentDays = item.RentDays,
                CloseDate = item.CloseDate,
                Penalty = item.Penalty,
            };

            result.Book = GetBookPart<DBook>(client, ApiDictionary.BookApi, item.Id, item.BookId, "Book");
            result.Client = GetBookPart<DEntityIdName>(client, ApiDictionary.ClientApi, item.Id, item.ClientId, "Client");
            result.Librarian = GetBookPart<DEntityIdName>(client, ApiDictionary.LibratianApi, item.Id, item.LibrarianId, "OpenLibrarian");
            result.Branch = GetBookPart<DEntityIdName>(client, ApiDictionary.BranchApi, item.Id, item.BranchId, "Branch");

            return result;
        }

        private T GetBookPart<T>(DarkHttpClient client, BaseApiMethods api, int itemId, int partId, string name)
            where T : class
        {
            var part = client.GetByIdFrom<T>(api, partId);

            if (part is null) throw new Exception($"Request error for rent (id: {itemId}) {name} (id: {partId})");
            if (!part.IsSuccess) throw new Exception($"Request error for rent (id: {itemId}) {name} (id: {partId})");
            if (part.Data is null) throw new Exception($"{name} (id: {partId}) for rent (id: {itemId}) not exists");

            return part.Data;
        }

        private CreateRentModel PrepareCreateRentModel(DarkHttpClient client)
        {
            var books = client.GetAllFrom<DBook>(ApiDictionary.BookApi);
            var clients = client.GetAllFrom<DEntityIdName>(ApiDictionary.ClientApi);
            var librarians = client.GetAllFrom<DEntityIdName>(ApiDictionary.LibratianApi);
            var branch = client.GetAllFrom<DEntityIdName>(ApiDictionary.BranchApi);

            return new CreateRentModel
            {
                Books = books.Data,
                Clients = clients.Data,
                Librarians = librarians.Data,
                Branches = branch.Data,
            };
        }

        private IActionResult CreateWithError(DarkHttpClient client, string message)
        {
            var model = PrepareCreateRentModel(client);
            model.ErrorText = message;
            return View("Create", model);
        }

        private CloseRentModel PrepareCloseRentModel(DarkHttpClient client, int id)
        {
            var rent = client.GetByIdFrom<DBookRent>(ApiDictionary.BookRentApi, id);
            return new CloseRentModel
            {
                RentId = id,
                Rent = MapRentToLinks(rent.Data, client),
            };
        }
    }
}
