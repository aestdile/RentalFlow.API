using System.Text.RegularExpressions;
using RentalFlow.API.Application.DTOs.HomeDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

public class HomeServiceWithValidation : HomeService
{
    public HomeServiceWithValidation(IGenericRepository<Home> repo) : base(repo) { }

    public new async Task<HomeDto> CreateAsync(HomeCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (dto.HostId <= 0)
            throw new ArgumentException("HostId must be greater than zero.");

        if (string.IsNullOrWhiteSpace(dto.Address))
            throw new ArgumentException("Address is required.");

        if (string.IsNullOrWhiteSpace(dto.Description) || dto.Description.Length < 10)
            throw new ArgumentException("Description must be at least 10 characters.");

        if (dto.NoofBedRooms < 0 || dto.NoofBathRooms < 0)
            throw new ArgumentException("Number of rooms cannot be negative.");

        if (dto.Area <= 0)
            throw new ArgumentException("Area must be positive.");

        if (dto.Price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        if (!Enum.IsDefined(typeof(HomeType), dto.HomeType))
            throw new ArgumentOutOfRangeException(nameof(dto.HomeType), "Invalid home type.");

        return await base.CreateAsync(dto);
    }

    public new async Task<HomeDto> UpdateAsync(long id, HomeUpdateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Address))
            throw new ArgumentException("Address is required.", nameof(dto.Address));

        if (string.IsNullOrWhiteSpace(dto.Description) || dto.Description.Length < 10)
            throw new ArgumentException("Description must be at least 10 characters.", nameof(dto.Description));

        if (dto.NoofBedRooms < 0 || dto.NoofBathRooms < 0)
            throw new ArgumentException("Room counts cannot be negative.");

        if (dto.Area <= 0)
            throw new ArgumentException("Area must be greater than zero.", nameof(dto.Area));

        if (dto.Price <= 0)
            throw new ArgumentException("Price must be greater than zero.", nameof(dto.Price));

        if (!Enum.IsDefined(typeof(HomeType), dto.HomeType))
            throw new ArgumentOutOfRangeException(nameof(dto.HomeType), "Invalid HomeType.");

        return await base.UpdateAsync(id, dto);
    }
}
