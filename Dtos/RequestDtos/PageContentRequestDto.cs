using System.ComponentModel.DataAnnotations;

namespace UserRoles.Dtos.RequestDtos
{
    public class PageContentRequestDto
    {
        public Guid? Id { get; set; }

        public PageContentTypeEnum ContentType { get; set; }

        [Required]
        public string Title { get; set; }

        public string? SubTitle { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }

        public IFormFile? UploadFile { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsPublished { get; set; } = true;

        public string? Slug { get; set; }

        public string? MetaTitle { get; set; }

        public string? MetaDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
