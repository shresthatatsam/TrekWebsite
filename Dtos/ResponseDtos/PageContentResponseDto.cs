using System.ComponentModel.DataAnnotations;

namespace UserRoles.Dtos.ResponseDtos
{
    public class PageContentResponseDto
    {
        public Guid Id { get; set; }

        public PageContentTypeEnum ContentType { get; set; }
   
        public string Title { get; set; }

        public string? SubTitle { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? FileUrl { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsPublished { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public string? Slug { get; set; }

        public string? MetaTitle { get; set; }

        public string? MetaDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
