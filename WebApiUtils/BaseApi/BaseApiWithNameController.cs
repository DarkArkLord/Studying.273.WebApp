using Microsoft.AspNetCore.Mvc;
using WebApiUtils.Entities;

namespace WebApiUtils.BaseApi
{
    public abstract class BaseApiWithNameController<TEntity, TRepo> : BaseApiController<TEntity, TRepo>
        where TEntity : DEntityIdName
        where TRepo : BaseWithNameRepository<TEntity>
    {
        [HttpGet("/get-by-name")]
        public DResponse<TEntity> GetByName(string name)
        {
            return DResponse<TEntity>.Success(repository.GetByName(name));
        }

        public override DResponse<TEntity> Add(TEntity item)
        {
            if (item.Name is null || item.Name.Length < 1) return DResponse<TEntity>.Error("Entity name can not be empty.", item);

            var dbItem = repository.GetByName(item.Name);
            if (dbItem is not null) return DResponse<TEntity>.Error($"Entity with name \"{item.Name}\" already exists.", item);

            return base.Add(item);
        }
    }
}
