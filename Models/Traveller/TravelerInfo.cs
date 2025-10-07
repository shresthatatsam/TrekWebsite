namespace UserRoles.Models.Traveller
{
    public class TravelerInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
    public class TripBookingRequest
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int PeopleCount { get; set; }
        public int PricePerPerson { get; set; }
        public int TotalPrice { get; set; }
        public TravelerInfo Traveler { get; set; }
    }
}
