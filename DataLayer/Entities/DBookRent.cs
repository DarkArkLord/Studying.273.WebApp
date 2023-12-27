﻿using System;

namespace DataLayer.Entities
{
    public class DBookRent
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public int LibrarianId { get; set; }
        public int BranchId { get; set; }
        public DateTime RentDate { get; set; }
        public int RentDays { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? Penalty { get; set; }
    }
}
