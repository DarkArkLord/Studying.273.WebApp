using DataLayer.Entities;

namespace DataLayer.LinkedEntities
{
    public class DUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PaswordHash { get; set; }
        public DLibrarian? Librarian { get; set; }
    }
}
