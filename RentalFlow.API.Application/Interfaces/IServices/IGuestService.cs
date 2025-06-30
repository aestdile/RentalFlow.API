using RentalFlow.API.Application.DTOs.GuestDTOs;

namespace RentalFlow.API.Application.Interfaces.IServices;

public interface IGuestService
{
    Task<GuestDto> CreateAsync(GuestCreateDto guestCreateDto);
    Task<GuestDto> GetByIdAsync(long id);
    Task<IEnumerable<GuestDto>> GetAllAsync();
    Task<GuestDto> UpdateAsync(long id, GuestUpdateDto guestUpdateDto);
    Task<long> DeleteAsync(long id);
}
