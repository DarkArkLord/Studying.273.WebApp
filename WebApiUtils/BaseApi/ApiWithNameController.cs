using System;

namespace WebApiUtils.BaseApi
{
    public class ApiWithNameController : BaseApiWithNameController
    {
        protected override string connectionString => Environment.GetEnvironmentVariable("ConnectionString")!;
        protected override BaseWithNameRepository repository => new BaseWithNameRepository(connectionString);
    }
}
