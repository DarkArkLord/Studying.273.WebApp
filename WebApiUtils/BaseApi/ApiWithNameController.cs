using System;
using WebApiUtils.Entities;

namespace WebApiUtils.BaseApi
{
    public abstract class ApiWithNameController : BaseApiWithNameController<DEntityIdName, BaseWithNameRepository<DEntityIdName>>
    {
        protected override string connectionString => Environment.GetEnvironmentVariable("ConnectionString")!;
        protected override BaseWithNameRepository<DEntityIdName> repository => new BaseWithNameRepository<DEntityIdName>(connectionString);
    }
}
