using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;
using UserRoles.Models;

namespace UserRoles.Services.Interface
{
    public interface IPageContentService
    {
        Task<IEnumerable<PageContentResponseDto>> GetAllAsync();
        Task<PageContentResponseDto?> GetByIdAsync(Guid id);
        Task<PageContentResponseDto> CreateAsync(PageContentRequestDto content);
        Task<PageContentResponseDto> UpdateAsync(PageContentRequestDto content);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<PageContent>> GetFilteredPageContentsAsync(PageContentTypeEnum? contentTypeFilter, bool? isPublishedFilter);
    }
}
