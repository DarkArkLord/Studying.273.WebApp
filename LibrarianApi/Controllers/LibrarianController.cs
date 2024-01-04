using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUtils.BaseApi;

namespace LibrarianApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LibrarianController : ApiWithNameController { }
}
