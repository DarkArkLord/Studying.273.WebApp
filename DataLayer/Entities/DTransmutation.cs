using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class DTransmutation
    {
        public int Id { get; set; }
        public DUser Alchemist { get; set; }
        public DTransmutationRecipe Recipe { get; set; }
        [NotMapped]
        public List<DProductWeightTrans> InputProducts { get; set; }
        [NotMapped]
        public List<DElementWeightTrans> Waste { get; set; }
    }
}
