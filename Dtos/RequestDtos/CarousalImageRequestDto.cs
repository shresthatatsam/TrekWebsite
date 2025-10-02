namespace UserRoles.Dtos.RequestDtos
{
    public class CarousalImageRequestDto
    {
        public Guid Id { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Caption { get; set; }
        public string SubCaption { get; set; }
        public bool IsActive { get; set; }
        public CarousalEnum carousalEnum { get; set; }
    }
}
