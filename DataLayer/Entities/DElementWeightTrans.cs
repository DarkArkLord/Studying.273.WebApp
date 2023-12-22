using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Keyless]
    public class DElementWeightTrans
    {
        public DTransmutation Transmutation { get; set; }
        public DElement Element { get; set; }
        public int Weight { get; set; }
    }
}
