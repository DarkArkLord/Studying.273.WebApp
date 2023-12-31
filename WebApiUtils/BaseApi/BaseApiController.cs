using Microsoft.AspNetCore.Mvc;
using WebApiUtils.Entities;

namespace WebApiUtils.BaseApi
{
    public abstract class BaseApiController<T, TRepo> : ControllerBase
        where T : DEntityWithId
        where TRepo : BaseRepository<T>
    {
        protected abstract string connectionString { get; }
        protected abstract TRepo repository { get; }

        [HttpGet("/get-all")]
        public DResponse<T[]> GetAll()
        {
            return DResponse<T[]>.Success(repository.GetAll());
        }

        [HttpGet("/get-by-id")]
        public DResponse<T> GetById(int id)
        {
            return DResponse<T>.Success(repository.GetById(id));
        }

        [HttpPost("/add")]
        public virtual DResponse<object> Add(T item)
        {
            var isAdded = repository.Add(item);
            return DResponse<object>.Maybe(isAdded, null);
        }

        [HttpPost("/update")]
        public virtual DResponse<object> Update(T item)
        {
            var isUpdated = repository.Update(item);
            return DResponse<object>.Maybe(isUpdated, null);
        }
    }
}
