using DataLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class ElementRepository
    {
        public IEnumerable<DElement> GetAll()
        {
            using (var db = new StoreDbContext())
            {
                return db.Elements.ToArray();
            }
        }
    }
}
