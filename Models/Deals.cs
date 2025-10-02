namespace UserRoles.Models
{
    public class Deals
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public string SubCaption { get; set; }


        public string Features { get; set; }
        public string Header { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public bool Isactive { get; set; }

    }
}
