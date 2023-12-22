using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DProductWeightTransRecipe
    {
        public DTransmutationRecipe TransmutationRecipe { get; set; }
        public DProduct Product { get; set; }
        public double Weight { get; set; }
    }
}
