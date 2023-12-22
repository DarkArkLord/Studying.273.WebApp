using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class DTransmutationRecipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DElementWeight> InputElement { get; set; }
        public DProductWeight OutputProduct { get; set; }
    }
}
