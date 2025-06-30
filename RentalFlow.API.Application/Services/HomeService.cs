using RentalFlow.API.Application.DTOs.HomeDTOs;
using RentalFlow.API.Application.Interfaces.IServices;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.API.Application.Services;

public class HomeService : IHomeService
{
    private readonly IGenericRepository<Home> _homeRepository;
    public HomeService(IGenericRepository<Home> homeRepository)
    {
        _homeRepository = homeRepository;
    }
    public async Task<HomeDto> CreateAsync(HomeCreateDto homeCreateDto)
    {
        var home = new Home
        {
            HostId = homeCreateDto.HostId,
            Address = homeCreateDto.Address,
            Description = homeCreateDto.Description,
            IsAvailable = homeCreateDto.IsAvailable,
            NoofBedRooms = homeCreateDto.NoofBedRooms,
            NoofBathRooms = homeCreateDto.NoofBathRooms,
            Area = homeCreateDto.Area,
            IsPetAllowed = homeCreateDto.IsPetAllowed,
            HomeType = homeCreateDto.HomeType,
            Price = homeCreateDto.Price,
        };

        var createdHome = await _homeRepository.CreateAsync(home);
        var result = new HomeDto
        {
            Id = createdHome.Id,
            HostId = createdHome.HostId,
            Address = createdHome.Address,
            Description = createdHome.Description,
            IsAvailable = createdHome.IsAvailable,
            NoofBedRooms = createdHome.NoofBedRooms,
            NoofBathRooms = createdHome.NoofBathRooms,
            Area = createdHome.Area,
            IsPetAllowed = createdHome.IsPetAllowed,
            HomeType = createdHome.HomeType,
            Price = createdHome.Price
        };

        return result;
    }

    public async Task<long> DeleteAsync(long id)
    {
        return await _homeRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<HomeDto>> GetAllAsync()
    {
        var homes = await _homeRepository.GetAllAsync();

        return homes.Select(home => new HomeDto
        {
            Id = home.Id,
            HostId = home.HostId,
            Address = home.Address,
            Description = home.Description,
            IsAvailable = home.IsAvailable,
            NoofBedRooms = home.NoofBedRooms,
            NoofBathRooms = home.NoofBathRooms,
            Area = home.Area,
            IsPetAllowed = home.IsPetAllowed,
            HomeType = home.HomeType,
            Price = home.Price
        }).ToList();
    }

    public async Task<HomeDto> GetByIdAsync(long id)
    {
        var home = await _homeRepository.GetByIdAsync(id);
        if (home == null)
        {
            throw new KeyNotFoundException($"Home with ID {id} not found.");
        }

        return new HomeDto
        {
            Id = home.Id,
            HostId = home.HostId,
            Address = home.Address,
            Description = home.Description,
            IsAvailable = home.IsAvailable,
            NoofBedRooms = home.NoofBedRooms,
            NoofBathRooms = home.NoofBathRooms,
            Area = home.Area,
            IsPetAllowed = home.IsPetAllowed,
            HomeType = home.HomeType,
            Price = home.Price
        };
    }

    public async Task<HomeDto> UpdateAsync(long id, HomeUpdateDto homeUpdateDto)
    {
        var home = await _homeRepository.GetByIdAsync(id);
        if (home == null)
        {
            throw new KeyNotFoundException($"Home with ID {id} not found.");
        }

        home.Address = homeUpdateDto.Address;
        home.Description = homeUpdateDto.Description;
        home.IsAvailable = homeUpdateDto.IsAvailable;
        home.NoofBedRooms = homeUpdateDto.NoofBedRooms;
        home.NoofBathRooms = homeUpdateDto.NoofBathRooms;
        home.Area = homeUpdateDto.Area;
        home.IsPetAllowed = homeUpdateDto.IsPetAllowed;
        home.HomeType = homeUpdateDto.HomeType;
        home.Price = homeUpdateDto.Price;

        await _homeRepository.UpdateAsync(id, home);

        return new HomeDto
        {
            Id = home.Id,
            HostId = home.HostId,
            Address = home.Address,
            Description = home.Description,
            IsAvailable = home.IsAvailable,
            NoofBedRooms = home.NoofBedRooms,
            NoofBathRooms = home.NoofBathRooms,
            Area = home.Area,
            IsPetAllowed = home.IsPetAllowed,
            HomeType = home.HomeType,
            Price = home.Price
        };
    }
}
