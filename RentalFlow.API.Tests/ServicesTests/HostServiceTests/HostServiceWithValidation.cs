using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;
using System.Text.RegularExpressions;

public class HostServiceWithValidation : HostService
{
    public HostServiceWithValidation(IGenericRepository<Host> repo)
        : base(repo) { }

    public new async Task<HostDto> CreateAsync(HostCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName) || !Regex.IsMatch(dto.FirstName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("FirstName must be valid.", nameof(dto.FirstName));

        if (string.IsNullOrWhiteSpace(dto.LastName) || !Regex.IsMatch(dto.LastName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("LastName must be valid.", nameof(dto.LastName));

        if (dto.DateOfBirth > DateTime.Today)
            throw new ArgumentException("DateOfBirth cannot be in the future.", nameof(dto.DateOfBirth));

        if (string.IsNullOrWhiteSpace(dto.Email) || !Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid Email.", nameof(dto.Email));

        if (string.IsNullOrWhiteSpace(dto.Password) || !Regex.IsMatch(dto.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$"))
            throw new ArgumentException("Weak Password.", nameof(dto.Password));

        if (string.IsNullOrWhiteSpace(dto.PhoneNumber) || !Regex.IsMatch(dto.PhoneNumber, @"^\d{9,15}$"))
            throw new ArgumentException("Invalid PhoneNumber.", nameof(dto.PhoneNumber));

        if (!Enum.IsDefined(typeof(Gender), dto.Gender))
            throw new ArgumentOutOfRangeException(nameof(dto.Gender), "Invalid gender value.");

        return await base.CreateAsync(dto);
    }

    public new async Task<HostDto> UpdateAsync(long id, HostUpdateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName) || !Regex.IsMatch(dto.FirstName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("FirstName is invalid.", nameof(dto.FirstName));

        if (string.IsNullOrWhiteSpace(dto.LastName) || !Regex.IsMatch(dto.LastName, @"^[A-Z][a-z]+$"))
            throw new ArgumentException("LastName is invalid.", nameof(dto.LastName));

        if (dto.DateOfBirth > DateTime.Today)
            throw new ArgumentException("DateOfBirth is in future.", nameof(dto.DateOfBirth));

        if (string.IsNullOrWhiteSpace(dto.PhoneNumber) || !Regex.IsMatch(dto.PhoneNumber, @"^\d{9,15}$"))
            throw new ArgumentException("PhoneNumber is invalid.", nameof(dto.PhoneNumber));

        if (!Enum.IsDefined(typeof(Gender), dto.Gender))
            throw new ArgumentOutOfRangeException(nameof(dto.Gender), "Gender value is not valid.");

        return await base.UpdateAsync(id, dto);
    }

}
