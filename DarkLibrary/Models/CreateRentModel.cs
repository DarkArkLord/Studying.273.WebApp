using DataLayer.Entities;

namespace DarkLibrary.Models
{
    public class CreateRentModel
    {
        public IEnumerable<DBook> Books { get; set; }
        public IEnumerable<DClient> Clients { get; set; }
        public IEnumerable<DLibrarian> Librarians { get; set; }
        public IEnumerable<DBranch> Branches { get; set; }
        public string ErrorText { get; set; }
    }
}
