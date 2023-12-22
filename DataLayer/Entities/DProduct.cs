using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class DProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<DElementPurityProduct> Elements { get; set; }
        public int Price { get; set; }
    }
}
