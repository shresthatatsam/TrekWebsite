using UserRoles.Models.Trek;

namespace UserRoles.Dtos.RequestDtos
{
    public class TrekPackageRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public string Difficulty { get; set; }
        public string Activity { get; set; }
        public string MaxAltitude { get; set; }

        public string BestSeason { get; set; }
        public string Accomodation { get; set; }
        public string Meal { get; set; }
        public string StartEndPoint { get; set; }

        //overview
        public string? TrekOverview { get; set; }

        //packingList
        public string? TrekPackingList { get; set; }

        //Exclusion
        public string TrekingPackageExclusion { get; set; }

        //Inclusion
        public string TrekingPackageInclusion { get; set; }

        //Highlights
        public string TrekHighlight { get; set; }

        public TrekPackageCostInfo PackageCostInfo { get; set; }
        public List<TrekFAQ> FAQs { get; set; } = new List<TrekFAQ>();
        public List<TrekItineraryDay> TrekItineraryDays { get; set; } = new List<TrekItineraryDay>();
        public List<TrekPackageImageRequestDto> Image { get; set; } = new();
    }




    public class TrekPackageCostInfo
    {
        public int Id { get; set; }
        public decimal BasePrice { get; set; }
        public string? Currency { get; set; }
        public string? PriceNote { get; set; }
        public List<TrekPackageGroupPricing> GroupPricing { get; set; } = new List<TrekPackageGroupPricing>();
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekPackageGroupPricing
    {
        public int Id { get; set; }
        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }
        //public string? GroupSize { get; set; } // e.g., "1 Pax", "2-3 Pax"
        public string? PricePerPerson { get; set; }
        public int TrekPackageCostInfoId { get; set; }
        public TrekPackageCostInfo TrekPackageCostInfo { get; set; }
    }

    public class TrekFAQ
    {
        public string? Category { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }



    public class TrekItineraryDay
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekPackageImageRequestDto
    {
        public int Id { get; set; }
        public IFormFile ImageFiles { get; set; }
        public string? Caption { get; set; }
        public string? SubCaption { get; set; }
        public bool IsActive { get; set; }
    }
}
