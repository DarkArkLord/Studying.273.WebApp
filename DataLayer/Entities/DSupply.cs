using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class DSupply
    {
        public int Id { get; set; }
        public DUser Supplier { get; set; }
        public DBranch Branch { get; set; }
        [NotMapped]
        public List<DProductWeightSupply> Products { get; set; }
        public int Price { get; set; }
    }
}
