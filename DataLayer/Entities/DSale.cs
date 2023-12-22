namespace DataLayer.Entities
{
    public class DSale
    {
        public int Id { get; set; }
        public DUser Customer { get; set; }
        public DBranch Branch { get; set; }
        public ICollection<DProductWeight> Products { get; set; }
        public int Price { get; set; }
    }
}
