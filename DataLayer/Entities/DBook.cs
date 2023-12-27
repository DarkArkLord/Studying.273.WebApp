namespace DataLayer.Entities
{
    public class DBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int SeriesId { get; set; }
    }
}
