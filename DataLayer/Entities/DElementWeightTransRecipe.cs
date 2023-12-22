using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DElementWeightTransRecipe
    {
        public DElement Element { get; set; }
        public int Weight { get; set; }
    }
}
