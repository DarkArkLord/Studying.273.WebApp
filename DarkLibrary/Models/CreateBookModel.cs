using WebApiUtils.Entities;

namespace DarkLibrary.Models
{
    public class CreateBookModel
    {
        public string Name { get; set; }
        public IEnumerable<DEntityIdName> Authors { get; set; }
        public IEnumerable<DEntityIdName> Series { get; set; }
        public string ErrorText { get; set; }
    }
}
