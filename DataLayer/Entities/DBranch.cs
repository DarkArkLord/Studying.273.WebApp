using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class DBranch
    {
        public int Id { get; set; }
        [NotMapped]
        public List<DProductWeightBranch> Products { get; set; }
    }
}
