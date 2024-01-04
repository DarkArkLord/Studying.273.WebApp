using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUtils.BaseApi;

namespace BranchApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BranchController : ApiWithNameController { }
}
