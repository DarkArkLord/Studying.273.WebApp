using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class DTransmutationRecipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<DElementWeightTransRecipe> InputElement { get; set; }
        [NotMapped]
        public DProductWeightTransRecipe OutputProduct { get; set; }
    }
}
