using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DProductWeightSale
    {
        public DSale Sale { get; set; }
        public DProduct Product { get; set; }
        public int Weight { get; set; }
    }
}
