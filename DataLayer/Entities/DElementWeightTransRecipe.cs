using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DElementWeightTransRecipe
    {
        public DTransmutationRecipe TransmutationRecipe { get; set; }
        public DElement Element { get; set; }
        public int Weight { get; set; }
    }
}
