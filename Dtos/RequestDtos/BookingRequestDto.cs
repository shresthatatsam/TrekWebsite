using System.ComponentModel.DataAnnotations;

namespace UserRoles.Dtos.RequestDtos
{
    public class BookingRequestDto
    {
        public string TrekPackageSlug { get; set; }
        public TrekPackageRequestDto? TrekPackage { get; set; } // Optional for view rendering

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Number of people is required.")]
        [Range(1, 25, ErrorMessage = "Number of people must be between 1 and 25.")]
        public int NumberOfPeople { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Month of birth is required.")]
        [Range(1, 12, ErrorMessage = "Invalid month.")]
        public int DobMonth { get; set; }

        [Required(ErrorMessage = "Day of birth is required.")]
        [Range(1, 31, ErrorMessage = "Invalid day.")]
        public int DobDay { get; set; }

        [Required(ErrorMessage = "Year of birth is required.")]
        [Range(1900, 2015, ErrorMessage = "Year must be between 1900 and 2015.")]
        public int DobYear { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Nationality is required.")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Passport number is required.")]
        public string PassportNumber { get; set; }

        public IFormFile? PassportScan { get; set; }
    }

    //public class TripBookingDto
    //{
    //    public string StartDate { get; set; }
    //    public string EndDate { get; set; }
    //    public int PeopleCount { get; set; }
    //    public int PricePerPerson { get; set; }
    //    public int TotalPrice { get; set; }
    //    public List<TravelerDto> Travelers { get; set; }
    //}

    //public class TravelerDto
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Email { get; set; }
    //    public string DobMonth { get; set; }
    //    public string DobDay { get; set; }
    //    public string DobYear { get; set; }
    //    public string Gender { get; set; }
    //    public string Phone { get; set; }
    //    public string Nationality { get; set; }
    //    public string PassportNumber { get; set; }
    //}

}
