using Microsoft.AspNetCore.Mvc;

namespace WebApiUtils.BaseApi
{
    public abstract class BaseApiController<T, TRepo> : ControllerBase
        where T : DEntityWithId
        where TRepo : BaseRepository<T>
    {
        protected abstract string connectionString { get; }
        protected abstract TRepo repository { get; }

        [HttpGet("/get-all")]
        public T[] GetAll()
        {
            return repository.GetAll();
        }

        [HttpGet("/get-by-id")]
        public T? GetById(int id)
        {
            return repository.GetById(id);
        }

        [HttpPost("/add")]
        public bool Add(T item)
        {
            return repository.Add(item);
        }

        [HttpPost("/update")]
        public bool Update(T item)
        {
            return repository.Update(item);
        }
    }
}
