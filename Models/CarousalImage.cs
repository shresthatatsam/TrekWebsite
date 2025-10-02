namespace UserRoles.Models
{
    public class CarousalImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public string SubCaption { get; set; }
        public bool IsActive { get; set; }

        public CarousalEnum carousalEnum { get; set; }
    }
}
