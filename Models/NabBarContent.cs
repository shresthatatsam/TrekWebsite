namespace UserRoles.Models
{
    public class NabBarContent
    {
        public Guid Id { get; set; }
        public string? MainHeading { get; set; }
        public List<NavItem> Items { get; set; } = new();
    }


    public class NavItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }        
        public string Slug { get; set; }        
        public string? Url { get; set; }
        public Guid NavBarContentId { get; set; }

        public NabBarContent NavBarContent { get; set; }
    }


}
