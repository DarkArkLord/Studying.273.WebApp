using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DProductWeightTransRecipe
    {
        public DProduct Product { get; set; }
        public int Weight { get; set; }
    }
}
