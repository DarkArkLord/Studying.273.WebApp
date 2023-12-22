using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DElementPurityProduct
    {
        public DProduct Product { get; set; }
        public DElement Element { get; set; }
        public double Purity { get; set; }
    }
}
