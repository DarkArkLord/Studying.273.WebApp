using DataLayer.Entities;

namespace DarkLibrary.Models
{
    public class CreateBookModel
    {
        public IEnumerable<DAuthor> Authors { get; set; }
        public IEnumerable<DBookSeries> Series { get; set; }
        public string ErrorText { get; set; }
    }
}
