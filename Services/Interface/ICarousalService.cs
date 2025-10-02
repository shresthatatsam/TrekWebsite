using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;

namespace UserRoles.Services.Interface
{
    public interface ICarousalService
    {
        Task<CarousalImageResponseDto> Create(CarousalImageRequestDto dto);
        Task<CarousalImageResponseDto?> GetById(Guid id);
        Task<bool> Update(CarousalImageRequestDto dto, IFormFile? newImageFile = null);
        Task<bool> Delete(Guid id);
		Task<List<CarousalImageResponseDto>> List(CarousalEnum carousalEnum);
	}
}
