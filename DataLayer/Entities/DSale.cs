namespace DataLayer.Entities
{
    public class DSale
    {
        public int Id { get; set; }
        public DClient Client { get; set; }
        public ICollection<DProductWeight> Products { get; set; }
    }
}
