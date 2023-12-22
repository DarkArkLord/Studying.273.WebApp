using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DProductWeightBranch
    {
        public DBranch Branch { get; set; }
        public DProduct Product { get; set; }
        public int Weight { get; set; }
    }
}
