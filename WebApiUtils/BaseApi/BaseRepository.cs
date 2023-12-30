using System.Linq;

namespace WebApiUtils.BaseApi
{
    public class BaseRepository<T>
        where T : DEntityWithId
    {
        protected string connectionString;

        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public T? GetById(int id)
        {
            using (var db = new BaseContext<T>(connectionString))
            {
                return db.Items.FirstOrDefault(x => x.Id == id);
            }
        }

        public T[] GetAll()
        {
            using (var db = new BaseContext<T>(connectionString))
            {
                return db.Items.ToArray();
            }
        }

        public virtual bool Add(T item)
        {
            using (var db = new BaseContext<T>(connectionString))
            {
                db.Items.Add(item);
                db.SaveChanges();
                return true;
            }
        }

        public bool Update(T item)
        {
            using (var db = new BaseContext<T>(connectionString))
            {
                var dbItem = db.Items.FirstOrDefault(x => x.Id == item.Id);
                if (dbItem is null) return false;

                var itemId = dbItem.Id;
                DarkConverter.CopyInto(item, dbItem);
                dbItem.Id = itemId;
                db.Items.Update(item);
                db.SaveChanges();
                return true;
            }
        }
    }
}
