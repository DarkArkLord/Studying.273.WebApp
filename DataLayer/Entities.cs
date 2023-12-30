using System;
using WebApiUtils.BaseApi;

namespace DataLayer.Entities
{
    public class DAuthor : DEntityIdName { }

    public class DBookSeries : DEntityIdName { }

    public class DBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DAuthor? Author { get; set; }
        public DBookSeries? Series { get; set; }
    }

    public class DClient : DEntityIdName { }

    public class DLibrarian : DEntityIdName { }

    public class DBranch : DEntityIdName { }

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

    public class DUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PaswordHash { get; set; }
        public DLibrarian? Librarian { get; set; }
    }
}
