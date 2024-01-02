using System;

namespace WebApiUtils.Entities
{
    public class DBookRent : DEntityWithId
    {
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public int OpenLibrarianId { get; set; }
        public int BranchId { get; set; }
        public DateTime OpenDate { get; set; }
        public int RentDays { get; set; }
        public int? CloseLibrarianId { get; set; }
        public DateTime? CloseDate { get; set; }
        public int? Penalty { get; set; }
    }

    public class DBookRentLinked : DEntityWithId
    {
        public DBook BookId { get; set; }
        public DEntityIdName ClientId { get; set; }
        public DEntityIdName OpenLibrarianId { get; set; }
        public DEntityIdName BranchId { get; set; }
        public DateTime OpenDate { get; set; }
        public int RentDays { get; set; }
        public DEntityIdName? CloseLibrarianId { get; set; }
        public DateTime? CloseDate { get; set; }
        public int? Penalty { get; set; }
    }
}
