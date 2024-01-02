using Microsoft.AspNetCore.Mvc;
using System;
using WebApiUtils;
using WebApiUtils.ApiAddresses;
using WebApiUtils.BaseApi;
using WebApiUtils.Entities;

namespace BookRentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookRentController : BaseApiController<DBookRent, BaseRepository<DBookRent>>
    {
        protected override string connectionString => Environment.GetEnvironmentVariable("ConnectionString")!;
        protected override BaseRepository<DBookRent> repository => new BaseRepository<DBookRent>(connectionString);

        public override DResponse<DBookRent> Add(DBookRent item)
        {
            using (var client = new DarkHttpClient())
            {
                var valudationResult = ValidateRent(item, client);
                if (valudationResult is not null) return valudationResult;

                return base.Add(item);
            }
        }

        public override DResponse<DBookRent> Update(DBookRent item)
        {
            using (var client = new DarkHttpClient())
            {
                var valudationResult = ValidateRent(item, client);
                if (valudationResult is not null) return valudationResult;

                return base.Update(item);
            }
        }

        private DResponse<DBookRent>? ValidateRent(DBookRent item, DarkHttpClient client)
        {
            var bookResponce = CheckOtherObjectExists(item.BookId, client, ApiDictionary.BookApi.GetById, "Book", item);
            if (bookResponce is not null) return bookResponce;

            var clientResponce = CheckOtherObjectExists(item.ClientId, client, ApiDictionary.ClientApi.GetById, "Client", item);
            if (clientResponce is not null) return clientResponce;

            var openLibrarialResponce = CheckOtherObjectExists(item.OpenLibrarianId, client, ApiDictionary.LibratianApi.GetById, "Open librarian", item);
            if (openLibrarialResponce is not null) return openLibrarialResponce;

            var branchResponce = CheckOtherObjectExists(item.BranchId, client, ApiDictionary.BranchApi.GetById, "Branch", item);
            if (branchResponce is not null) return branchResponce;

            if (item.RentDays < 1)
            {
                return DResponse<DBookRent>.Error("RentDays must be positive", item);
            }

            if (item.CloseLibrarianId is not null)
            {
                var closeLibrarialResponce = CheckOtherObjectExists(item.CloseLibrarianId, client, ApiDictionary.LibratianApi.GetById, "Close librarian", item);
                if (closeLibrarialResponce is not null) return closeLibrarialResponce;

                if (item.CloseDate is null) return DResponse<DBookRent>.Error("CloseDate must be not null", item);

                if (item.Penalty is null) return DResponse<DBookRent>.Error("Penalty must be not null", item);
            }

            if (item.CloseDate is not null)
            {
                if (item.CloseDate < item.OpenDate)
                {
                    return DResponse<DBookRent>.Error("CloseDate must be greater then OpenDate", item);
                }

                if (item.CloseLibrarianId is null) return DResponse<DBookRent>.Error("Close librarian must be not null", item);

                if (item.Penalty is null) return DResponse<DBookRent>.Error("Penalty must be not null", item);
            }

            if (item.Penalty is not null)
            {
                if (item.Penalty < 0)
                {
                    return DResponse<DBookRent>.Error("Penalty must be greater or equal than 0", item);
                }

                if (item.CloseLibrarianId is null) return DResponse<DBookRent>.Error("Close librarian must be not null", item);

                if (item.CloseDate is null) return DResponse<DBookRent>.Error("CloseDate must be not null", item);
            }

            return null;
        }

        [HttpGet("/calculate")]
        public DResponse<DBookRent> CalculateRentGet(int rentId, DateTime closeDate, int rentByDay)
        {
            var item = repository.GetById(rentId);
            if (item is null) return DResponse<DBookRent>.Error($"Rent with id {rentId} not exists");
            if (item.CloseDate is not null) return DResponse<DBookRent>.Error($"Rent with id {rentId} already closed", item);

            item.CloseDate = closeDate;
            if (item.CloseDate < item.OpenDate)
            {
                return DResponse<DBookRent>.Error("CloseDate must be greater then OpenDate", item);
            }

            if (rentByDay < 0)
            {
                return DResponse<DBookRent>.Error("Penalty must be greater or equal than 0");
            }

            var rentDays = (int)(item.CloseDate - item.OpenDate).Value.TotalDays;
            if (rentDays < item.RentDays)
            {
                item.Penalty = 0;
            }
            else
            {
                item.Penalty = rentDays * rentByDay;
            }

            return DResponse<DBookRent>.Success(item);
        }

        [HttpPost("/close")]
        public DResponse<DBookRent> CloseRentPost(int rentId, int librarianid, DateTime closeDate, int rentByDay)
        {
            var item = repository.GetById(rentId);
            if (item is null) return DResponse<DBookRent>.Error($"Rent with id {rentId} not exists");
            if (item.CloseDate is not null) return DResponse<DBookRent>.Error($"Rent with id {rentId} already closed", item);

            item.CloseLibrarianId = librarianid;
            using (var client = new DarkHttpClient())
            {
                var closeLibrarialResponce = CheckOtherObjectExists(item.CloseLibrarianId, client, ApiDictionary.LibratianApi.GetById, "Close librarian", item);
                if (closeLibrarialResponce is not null) return closeLibrarialResponce;
            }

            item.CloseDate = closeDate;
            if (item.CloseDate < item.OpenDate)
            {
                return DResponse<DBookRent>.Error("CloseDate must be greater then OpenDate", item);
            }

            if (rentByDay < 0)
            {
                return DResponse<DBookRent>.Error("Penalty must be greater or equal than 0");
            }

            var rentDays = (int)(item.CloseDate - item.OpenDate).Value.TotalDays;
            if (rentDays < item.RentDays)
            {
                item.Penalty = 0;
            }
            else
            {
                item.Penalty = rentDays * rentByDay;
            }

            return base.Update(item);
        }
    }
}