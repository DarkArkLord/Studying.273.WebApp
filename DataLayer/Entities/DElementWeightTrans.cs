using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DElementWeightTrans
    {
        public DElement Element { get; set; }
        public int Weight { get; set; }
    }
}
