using RentalFlow.API.Application.DTOs.HomeDTOs;

namespace RentalFlow.API.Application.Interfaces.IServices;

public interface IHomeService
{
    Task<HomeDto> CreateAsync(HomeCreateDto homeCreateDto);
    Task<HomeDto> GetByIdAsync(long id);
    Task<IEnumerable<HomeDto>> GetAllAsync();
    Task<HomeDto> UpdateAsync(long id, HomeUpdateDto homeUpdateDto);
    Task<long> DeleteAsync(long id);
}
