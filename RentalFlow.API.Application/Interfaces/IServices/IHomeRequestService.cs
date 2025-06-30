using RentalFlow.API.Application.DTOs.HomeRequestDTOs;

namespace RentalFlow.API.Application.Interfaces.IServices;

public interface IHomeRequestService
{
    Task<HomeRequestDto> CreateAsync(HomeRequestCreateDto homeRequestCreateDto);
    Task<HomeRequestDto> GetByIdAsync(long id);
    Task<IEnumerable<HomeRequestDto>> GetAllAsync();
    Task<HomeRequestDto> UpdateAsync(long id, HomeRequestUpdateDto homeRequestUpdateDto);
    Task<long> DeleteAsync(long id);
}
