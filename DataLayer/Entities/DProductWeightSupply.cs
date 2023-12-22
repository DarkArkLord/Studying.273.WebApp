using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DProductWeightSupply
    {
        public DSupply Supply { get; set; }
        public DProduct Product { get; set; }
        public int Weight { get; set; }
    }
}
