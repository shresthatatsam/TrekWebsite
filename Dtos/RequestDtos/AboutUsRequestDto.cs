using System.ComponentModel.DataAnnotations;

namespace UserRoles.Dtos.RequestDtos
{
    public class AboutUsRequestDto
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Mission { get; set; }

        public string Story { get; set; }
        public IFormFile ImageFile { get; set; }

        public bool IsActive { get; set; } = true;

    }
 
}
