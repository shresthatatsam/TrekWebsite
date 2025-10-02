using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;

namespace UserRoles.Dtos.ResponseDtos
{
    public class AboutUsResponseDto
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Mission { get; set; }

        public string Story { get; set; }

        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
    }

   
}
