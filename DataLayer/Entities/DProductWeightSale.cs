using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DProductWeightSale
    {
        public DProduct Product { get; set; }
        public int Weight { get; set; }
    }
}
