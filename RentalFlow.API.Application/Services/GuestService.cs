using RentalFlow.API.Application.DTOs.GuestDTOs;
using RentalFlow.API.Application.Interfaces.IServices;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.API.Application.Services;

public class GuestService : IGuestService
{
    private readonly IGenericRepository<Guest> _guestRepository;

    public GuestService(IGenericRepository<Guest> guestRepository)
    {
        _guestRepository = guestRepository;
    }
    public async Task<GuestDto> CreateAsync(GuestCreateDto guestCreateDto)
    {
        if (guestCreateDto == null)
            throw new ArgumentNullException(nameof(guestCreateDto));

        var guestEntity = new Guest
        {
            FirstName = guestCreateDto.FirstName,
            LastName = guestCreateDto.LastName,
            DateOfBirth = guestCreateDto.DateOfBirth,
            Email = guestCreateDto.Email,
            Password = guestCreateDto.Password,
            PhoneNumber = guestCreateDto.PhoneNumber,
            Gender = guestCreateDto.Gender
        };

        var createdGuest = await _guestRepository.CreateAsync(guestEntity);

        if (createdGuest == null)
            throw new InvalidOperationException("Failed to create guest.");

        return new GuestDto
        {
            Id = createdGuest.Id,
            FirstName = createdGuest.FirstName,
            LastName = createdGuest.LastName,
            DateOfBirth = createdGuest.DateOfBirth,
            Email = createdGuest.Email,
            PhoneNumber = createdGuest.PhoneNumber,
            Gender = createdGuest.Gender
        };
    }

    public Task<long> DeleteAsync(long id)
    {
        return _guestRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<GuestDto>> GetAllAsync()
    {
        var guests = await _guestRepository.GetAllAsync();

        return guests.Select(guest => new GuestDto
        {
            Id = guest.Id,
            FirstName = guest.FirstName,
            LastName = guest.LastName,
            DateOfBirth = guest.DateOfBirth,
            Email = guest.Email,
            PhoneNumber = guest.PhoneNumber,
            Gender = guest.Gender
        }).ToList();

    }

    public async Task<GuestDto> GetByIdAsync(long id)
    {
        var guest = await _guestRepository.GetByIdAsync(id);

        if (guest == null)
        {
            throw new KeyNotFoundException($"Guest with ID {id} not found.");
        }

        return new GuestDto
        {
            Id = guest.Id,
            FirstName = guest.FirstName,
            LastName = guest.LastName,
            DateOfBirth = guest.DateOfBirth,
            Email = guest.Email,
            PhoneNumber = guest.PhoneNumber,
            Gender = guest.Gender
        };
    }

    public async Task<GuestDto> UpdateAsync(long id, GuestUpdateDto guestUpdateDto)
    {
        var guest = await _guestRepository.GetByIdAsync(id);
        if (guest == null)
        {
            throw new KeyNotFoundException($"Guest with ID {id} not found.");
        }

        guest.FirstName = guestUpdateDto.FirstName;
        guest.LastName = guestUpdateDto.LastName;
        guest.DateOfBirth = guestUpdateDto.DateOfBirth;
        guest.PhoneNumber = guestUpdateDto.PhoneNumber;
        guest.Gender = guestUpdateDto.Gender;

        await _guestRepository.UpdateAsync(id, guest);

        return new GuestDto
        {
            Id = guest.Id,
            FirstName = guest.FirstName,
            LastName = guest.LastName,
            DateOfBirth = guest.DateOfBirth,
            Email = guest.Email,
            PhoneNumber = guest.PhoneNumber,
            Gender = guest.Gender
        };
    }
}
