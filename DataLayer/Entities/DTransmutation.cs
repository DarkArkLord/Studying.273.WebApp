namespace DataLayer.Entities
{
    public class DTransmutation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DProductSaleCount> InputProducts { get; set; }
        public ICollection<DProductSaleCount> OutputProducts { get; set; }
    }
}
