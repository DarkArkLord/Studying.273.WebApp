using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class DSale
    {
        public int Id { get; set; }
        public DUser Customer { get; set; }
        public DBranch Branch { get; set; }
        [NotMapped]
        public List<DProductWeightSale> Products { get; set; }
        public int Price { get; set; }
    }
}
