using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
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
        public virtual DResponse<T> Add(T item)
        {
            var result = repository.Add(item);
            if (result is null)
            {
                return DResponse<T>.Error("Entity was not created", item);
            }
            else
            {
                return DResponse<T>.Success(result);
            }
        }

        [HttpPost("/update")]
        public virtual DResponse<T> Update(T item)
        {
            var result = repository.Update(item);
            if (result is null)
            {
                return DResponse<T>.Error("Entity was not updated", item);
            }
            else
            {
                return DResponse<T>.Success(result);
            }
        }

        protected DResponse<T>? CheckOtherObjectExists(int? objectId, DarkHttpClient httpClient, string requestUri, string objectName, T? currentEntity)
        {
            if (objectId is not null)
            {
                var response = httpClient.CreateRequest()
                    .SetMethodGet()
                    .SetUri($"{requestUri}?id={objectId}")
                    .SendAsync().Result.Content
                    .ReadFromJsonAsync(typeof(DResponse<T>)).Result as DResponse<T>;

                if (response?.Data is null)
                {
                    return DResponse<T>.Error($"{objectName} with id {objectId} not exists", currentEntity);
                }
            }

            return null;
        }
    }
}
