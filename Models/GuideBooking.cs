//namespace UserRoles.Models
//{
//    public class GuideBooking
//    {
//        public Guid Id { get; set; }

//        public Guid GuideId { get; set; }

//        public Guid UserId { get; set; } // The person booking (optional if needed)

//        public DateTime StartDate { get; set; }

//        public DateTime EndDate { get; set; }

//        public int TotalDays => (EndDate - StartDate).Days + 1;

//        public decimal TotalCost { get; set; }

//        public string? Notes { get; set; }

//        public bool IsConfirmed { get; set; }

//        // Navigation properties
//        public Guide Guide { get; set; }
//    }
//}
