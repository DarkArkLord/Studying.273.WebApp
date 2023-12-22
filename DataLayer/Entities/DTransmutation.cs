using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class DTransmutation
    {
        public int Id { get; set; }
        public DUser Alchemist { get; set; }
        public DTransmutationRecipe Recipe { get; set; }
        public ICollection<DProductWeight> InputProducts { get; set; }
        public ICollection<DElementWeight> Waste { get; set; }
    }
}
