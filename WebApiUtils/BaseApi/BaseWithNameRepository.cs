using System.Linq;

namespace WebApiUtils.BaseApi
{
    public class BaseWithNameRepository : BaseRepository<DEntityIdName>
    {
        public BaseWithNameRepository(string connectionString) : base(connectionString) { }

        public DEntityIdName? GetByName(string name)
        {
            using (var db = new BaseContext<DEntityIdName>(connectionString))
            {
                return db.Items.FirstOrDefault(x => x.Name == name);
            }
        }

        public override bool Add(DEntityIdName item)
        {
            using (var db = new BaseContext<DEntityIdName>(connectionString))
            {
                var dbItem = db.Items.FirstOrDefault(x => x.Name == item.Name);
                if (dbItem is not null) return false;
                db.Items.Add(item);
                db.SaveChanges();
                return true;
            }
        }
    }
}
