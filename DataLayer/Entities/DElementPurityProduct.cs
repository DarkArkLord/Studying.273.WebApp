using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DElementPurityProduct
    {
        public DElement Element { get; set; }
        public int Purity { get; set; }
    }
}
