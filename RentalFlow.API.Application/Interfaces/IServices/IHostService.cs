using RentalFlow.API.Application.DTOs.HostDTOs;

namespace RentalFlow.API.Application.Interfaces.IServices;

public interface IHostService
{
    Task<HostDto> CreateAsync(HostCreateDto hostCreateDto);
    Task<HostDto> GetByIdAsync(long id);
    Task<IEnumerable<HostDto>> GetAllAsync();
    Task<HostDto> UpdateAsync(long id, HostUpdateDto hostUpdateDto);
    Task<long> DeleteAsync(long id);
}
