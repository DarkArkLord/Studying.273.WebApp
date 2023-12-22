using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class DSupply
    {
        public int Id { get; set; }
        public DUser Supplier { get; set; }
        public DBranch Branch { get; set; }
        public ICollection<DProductWeight> Products { get; set; }
        public int Price { get; set; }
    }
}
