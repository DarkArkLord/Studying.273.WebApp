using Microsoft.AspNetCore.Mvc;

namespace WebApiUtils.BaseApi
{
    public abstract class BaseApiWithNameController : BaseApiController<DEntityIdName, BaseWithNameRepository>
    {
        [HttpGet("/get-by-name")]
        public DEntityIdName? GetByName(string name)
        {
            return repository.GetByName(name);
        }
    }
}
