using WebApiUtils.Entities;

namespace DarkLibrary.Models
{
    public class CreateRentModel
    {
        public IEnumerable<DBook> Books { get; set; }
        public IEnumerable<DEntityIdName> Clients { get; set; }
        public IEnumerable<DEntityIdName> Librarians { get; set; }
        public IEnumerable<DEntityIdName> Branches { get; set; }
        public string ErrorText { get; set; }
    }
}
