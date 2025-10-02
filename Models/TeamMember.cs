namespace UserRoles.Models
{
    public class TeamMember
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
        public string Bio { get; set; }

    }
}
