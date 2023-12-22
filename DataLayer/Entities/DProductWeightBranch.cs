using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DProductWeightBranch
    {
        public DProduct Product { get; set; }
        public double Weight { get; set; }
    }
}
