using WebApiUtils.Entities;

namespace DarkLibrary.Models
{
    public class CloseRentModel
    {
        public string? ErrorText { get; set; }
        public int RentId { get; set; }
        public DBookRentLinked? Rent { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PenaltyByDay { get; set; }
        public int? TotalPenalty { get; set; }
    }
}
