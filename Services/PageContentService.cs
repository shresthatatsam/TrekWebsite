using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;
using UserRoles.Models;
using UserRoles.Services.Interface;

namespace UserRoles.Services
{
    public class PageContentService : IPageContentService
    {
        private readonly AppDbContext _context;

        private readonly IFileService _fileService;
        public PageContentService(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<IEnumerable<PageContentResponseDto>> GetAllAsync()
        {
            return await _context.PageContents
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new PageContentResponseDto
                {
                    Id = x.Id,
                    ContentType = x.ContentType,
                    Title = x.Title,
                    SubTitle = x.SubTitle,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    FileUrl = x.FileUrl,
                    DisplayOrder = x.DisplayOrder,
                    IsPublished = x.IsPublished,
                    IsDeleted = x.IsDeleted,
                    Slug = x.Slug,
                    MetaTitle = x.MetaTitle,
                    MetaDescription = x.MetaDescription,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<PageContentResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.PageContents.FindAsync(id);
            if (entity == null || entity.IsDeleted)
                return null;

            return new PageContentResponseDto
            {
                Id = entity.Id,
                ContentType = entity.ContentType,
                Title = entity.Title,
                SubTitle = entity.SubTitle,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                FileUrl = entity.FileUrl,
                DisplayOrder = entity.DisplayOrder,
                IsPublished = entity.IsPublished,
                IsDeleted = entity.IsDeleted,
                Slug = entity.Slug,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<PageContentResponseDto> CreateAsync(PageContentRequestDto dto)
        {


            string imageUrl = null;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                imageUrl = await _fileService.SaveImageAsync(dto.ImageFile, "pagecontent-images");

            }

            string pdfUrl = null;

            if (dto.UploadFile != null && dto.UploadFile.Length > 0)
            {
                pdfUrl = await _fileService.SaveImageAsync(dto.UploadFile, "pagecontent-images");

            }
            var entity = new PageContent
            {
                Id = Guid.NewGuid(),
                ContentType = dto.ContentType,
                Title = dto.Title,
                SubTitle = dto.SubTitle,
                Description = dto.Description,
                ImageUrl = imageUrl,
                FileUrl = pdfUrl,
                DisplayOrder = dto.DisplayOrder,
                IsPublished = dto.IsPublished,
                Slug = dto.Slug,
                MetaTitle = dto.MetaTitle,
                MetaDescription = dto.MetaDescription,
                CreatedAt = DateTime.UtcNow
            };

            _context.PageContents.Add(entity);
            await _context.SaveChangesAsync();
            return new PageContentResponseDto
            {
                Id = entity.Id,
                ContentType = entity.ContentType,
                Title = entity.Title,
                SubTitle = entity.SubTitle,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                FileUrl = entity.FileUrl,
                DisplayOrder = entity.DisplayOrder,
                IsPublished = entity.IsPublished,
                IsDeleted = entity.IsDeleted,
                Slug = entity.Slug,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<PageContentResponseDto> UpdateAsync(PageContentRequestDto dto)
        {
            var entity = await _context.PageContents.FindAsync(dto.Id);
            if (entity == null || entity.IsDeleted)
                throw new Exception("Page content not found.");

            entity.ContentType = dto.ContentType;
            entity.Title = dto.Title;
            entity.SubTitle = dto.SubTitle;
            entity.Description = dto.Description;
         
            entity.DisplayOrder = dto.DisplayOrder;
            entity.IsPublished = dto.IsPublished;
            entity.Slug = dto.Slug;
            entity.MetaTitle = dto.MetaTitle;
            entity.MetaDescription = dto.MetaDescription;
            entity.UpdatedAt = DateTime.UtcNow;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                    await _fileService.DeleteFileAsync(entity.ImageUrl);

                var imageUrl = await _fileService.SaveImageAsync(dto.ImageFile, "pagecontent-images");
                entity.ImageUrl = imageUrl;
            }

            if (dto.UploadFile != null && dto.UploadFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(entity.FileUrl))
                    await _fileService.DeleteFileAsync(entity.FileUrl);

                var fileUrl = await _fileService.SaveImageAsync(dto.UploadFile, "pagecontent-images");
                entity.FileUrl = fileUrl;
            }


            _context.PageContents.Update(entity);
            await _context.SaveChangesAsync();

            return new PageContentResponseDto
            {
                Id = entity.Id,
                ContentType = entity.ContentType,
                Title = entity.Title,
                SubTitle = entity.SubTitle,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                FileUrl = entity.FileUrl,
                DisplayOrder = entity.DisplayOrder,
                IsPublished = entity.IsPublished,
                IsDeleted = entity.IsDeleted,
                Slug = entity.Slug,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.PageContents.FindAsync(id);
            if (entity != null && !entity.IsDeleted)
            {
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PageContent>> GetFilteredPageContentsAsync(PageContentTypeEnum? contentTypeFilter, bool? isPublishedFilter)
        {
            var query = _context.PageContents.AsQueryable();

            if (contentTypeFilter.HasValue)
            {
                query = query.Where(pc => pc.ContentType ==  contentTypeFilter);
            }

            if (isPublishedFilter.HasValue)
            {
                query = query.Where(pc => pc.IsPublished == isPublishedFilter.Value);
            }

            return await query.ToListAsync();
        }
    }
}
