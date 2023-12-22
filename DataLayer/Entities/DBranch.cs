namespace DataLayer.Entities
{
    public class DBranch
    {
        public int Id { get; set; }
        public ICollection<DProductWeight> Products { get; set; }
    }
}
