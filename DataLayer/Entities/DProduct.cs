namespace DataLayer.Entities
{
    public class DProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DElementPurity> Elements { get; set; }
        public int Price { get; set; }
    }
}
