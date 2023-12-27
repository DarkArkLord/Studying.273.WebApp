using DataLayer.Entities;

namespace DataLayer.LinkedEntities
{
    public class DBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DAuthor? Author { get; set; }
        public DBookSeries? Series { get; set; }
    }
}
