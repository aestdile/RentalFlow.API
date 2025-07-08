using System.Text.RegularExpressions;
using RentalFlow.API.Application.DTOs.GuestDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;


public class GuestServiceWithValidation : GuestService
{
    public GuestServiceWithValidation(IGenericRepository<Guest> guestRepository)
        : base(guestRepository) { }

    public new async Task<GuestDto> CreateAsync(GuestCreateDto guestCreateDto)
    {
        if (guestCreateDto == null)
            throw new ArgumentNullException(nameof(guestCreateDto));

        if (string.IsNullOrWhiteSpace(guestCreateDto.FirstName) ||
            !Regex.IsMatch(guestCreateDto.FirstName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("FirstName must start with uppercase, contain only lowercase letters and no digits or symbols.", nameof(guestCreateDto.FirstName));

        if (string.IsNullOrWhiteSpace(guestCreateDto.LastName) ||
            !Regex.IsMatch(guestCreateDto.LastName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("LastName must start with uppercase, contain only lowercase letters and no digits or symbols.", nameof(guestCreateDto.LastName));

        if (guestCreateDto.DateOfBirth > DateTime.Today)
            throw new ArgumentException("DateOfBirth cannot be in the future.", nameof(guestCreateDto.DateOfBirth));

        if (string.IsNullOrWhiteSpace(guestCreateDto.Email) ||
            !Regex.IsMatch(guestCreateDto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.", nameof(guestCreateDto.Email));

        if (string.IsNullOrWhiteSpace(guestCreateDto.Password) ||
            !Regex.IsMatch(guestCreateDto.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$"))
            throw new ArgumentException("Password must be at least 8 characters and include uppercase, lowercase, number and symbol.", nameof(guestCreateDto.Password));

        if (string.IsNullOrWhiteSpace(guestCreateDto.PhoneNumber) ||
            !Regex.IsMatch(guestCreateDto.PhoneNumber, @"^\d{9,15}$"))
            throw new ArgumentException("PhoneNumber must contain only digits and be 9 to 15 digits long.", nameof(guestCreateDto.PhoneNumber));

        if (!Enum.IsDefined(typeof(Gender), guestCreateDto.Gender))
            throw new ArgumentOutOfRangeException(nameof(guestCreateDto.Gender), "Invalid gender value.");

        return await base.CreateAsync(guestCreateDto);
    }

    public new async Task<GuestDto> UpdateAsync(long id, GuestUpdateDto guestUpdateDto)
    {
        if (guestUpdateDto == null)
            throw new ArgumentNullException(nameof(guestUpdateDto));

        if (string.IsNullOrWhiteSpace(guestUpdateDto.FirstName) ||
            !Regex.IsMatch(guestUpdateDto.FirstName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("Invalid FirstName format", nameof(guestUpdateDto.FirstName));

        if (string.IsNullOrWhiteSpace(guestUpdateDto.LastName) ||
            !Regex.IsMatch(guestUpdateDto.LastName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("Invalid LastName format", nameof(guestUpdateDto.LastName));

        if (guestUpdateDto.DateOfBirth > DateTime.Today)
            throw new ArgumentException("DateOfBirth cannot be in the future", nameof(guestUpdateDto.DateOfBirth));

        if (string.IsNullOrWhiteSpace(guestUpdateDto.PhoneNumber) ||
            !Regex.IsMatch(guestUpdateDto.PhoneNumber, @"^\d{9,15}$"))
            throw new ArgumentException("PhoneNumber must contain only digits and be 9 to 15 digits long.", nameof(guestUpdateDto.PhoneNumber));

        if (!Enum.IsDefined(typeof(Gender), guestUpdateDto.Gender))
            throw new ArgumentOutOfRangeException(nameof(guestUpdateDto.Gender), "Invalid gender value.");

        return await base.UpdateAsync(id, guestUpdateDto);
    }
}
