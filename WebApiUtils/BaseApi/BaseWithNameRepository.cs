using System.Linq;
using WebApiUtils.Entities;

namespace WebApiUtils.BaseApi
{
    public class BaseWithNameRepository<T> : BaseRepository<T>
        where T : DEntityIdName
    {
        public BaseWithNameRepository(string connectionString) : base(connectionString) { }

        public T? GetByName(string name)
        {
            using (var db = new BaseContext<T>(connectionString))
            {
                return db.Items.FirstOrDefault(x => x.Name == name);
            }
        }

        public override T? Add(T item)
        {
            using (var db = new BaseContext<T>(connectionString))
            {
                var dbItem = db.Items.FirstOrDefault(x => x.Name == item.Name);
                if (dbItem is not null) return null;
                var createdEntity = db.Items.Add(item);
                db.SaveChanges();
                return createdEntity.Entity;
            }
        }
    }
}
