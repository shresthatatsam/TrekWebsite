using System.ComponentModel.DataAnnotations;

namespace UserRoles.Models
{
    public class PageContent
    {

        public Guid Id { get; set; }

        // Enum to distinguish between types (team, document, etc.)
        public PageContentTypeEnum ContentType { get; set; }

        // Title for each entry (name of person, document, etc.)
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        // Optional subtitle (e.g., Designation, Document type, etc.)
        [MaxLength(200)]
        public string? SubTitle { get; set; }

        // Rich description (can be used as bio, legal content, etc.)
        public string? Description { get; set; }

        // Image (for team profile or affiliation logo)
        public string? ImageUrl { get; set; }

        // File attachment (e.g., PDF for Legal Documents)
        public string? FileUrl { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsPublished { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        // SEO metadata
        [MaxLength(200)]
        public string? Slug { get; set; }

        [MaxLength(255)]
        public string? MetaTitle { get; set; }

        [MaxLength(500)]
        public string? MetaDescription { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
