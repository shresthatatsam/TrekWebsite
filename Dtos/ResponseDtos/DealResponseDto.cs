namespace UserRoles.Dtos.ResponseDtos
{
    public class DealResponseDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public string SubCaption { get; set; }
        public string Header { get; set; }
        public decimal Amount { get; set; }

        public List<string> Features { get; set; }
        public string Details { get; set; }
        public bool Isactive { get; set; }
    }
}
