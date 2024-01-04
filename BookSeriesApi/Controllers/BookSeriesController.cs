using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUtils.BaseApi;

namespace BookSeriesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookSeriesController : ApiWithNameController { }
}
