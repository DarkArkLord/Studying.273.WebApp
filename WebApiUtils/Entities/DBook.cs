namespace WebApiUtils.Entities
{
    public class DBook : DEntityIdName
    {
        public int? AuthorId { get; set; }
        public int? SeriesId { get; set; }
    }
}
