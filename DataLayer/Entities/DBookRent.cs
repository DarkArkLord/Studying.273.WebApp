using System;

namespace DataLayer.Entities
{
    public class DBookRent
    {
        public int Id { get; set; }
        public DBook Book { get; set; }
        public DClient Client { get; set; }
        public DLibrarian Librarian { get; set; }
        public DBranch Branch { get; set; }
        public DateTime RentDate { get; set; }
        public int RentDays { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? Penalty { get; set; }
    }
}
