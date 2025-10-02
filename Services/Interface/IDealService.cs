using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;

namespace UserRoles.Services.Interface
{
    public interface IDealService
    {
        Task<DealResponseDto> Create(DealRequestDto dto);
        Task<DealResponseDto?> GetById(Guid id);
        Task<bool> Update(DealRequestDto dto);
        Task<List<DealResponseDto>> List();
        Task<bool> Delete(Guid id);
    }
}
