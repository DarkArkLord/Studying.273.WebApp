namespace WebApiUtils.Entities
{
    public class DBook : DEntityIdName
    {
        public int? AuthorId { get; set; }
        public int? SeriesId { get; set; }
    }

    public class DBookLinked : DEntityIdName
    {
        public DEntityIdName? Author { get; set; }
        public DEntityIdName? Series { get; set; }
    }
}
