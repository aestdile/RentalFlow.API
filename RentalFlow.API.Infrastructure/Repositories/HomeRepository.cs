using Microsoft.EntityFrameworkCore;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Infrastructure.Persistance;

namespace RentalFlow.API.Infrastructure.Repositories;

public class HomeRepository : IGenericRepository<Home>
{
    private readonly RentalFlowDbContext _rentalFlowDbContext;
    public HomeRepository(RentalFlowDbContext rentalFlowDbContext)
    {
        _rentalFlowDbContext = rentalFlowDbContext;
    }
    public async Task<Home> CreateAsync(Home entity)
    {
        await _rentalFlowDbContext.Homes.AddAsync(entity);
        await _rentalFlowDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<long> DeleteAsync(long id)
    {
        var home = await _rentalFlowDbContext.Homes.FindAsync(id);
        if (home == null)
        {
            throw new KeyNotFoundException($"Home with ID {id} not found.");
        }

        _rentalFlowDbContext.Homes.Remove(home);
        _rentalFlowDbContext.SaveChanges();
        return id;
    }

    public async Task<IEnumerable<Home>> GetAllAsync()
    {
        return await _rentalFlowDbContext.Homes.ToListAsync();
    }

    public async Task<Home> GetByIdAsync(long id)
    {
        return await _rentalFlowDbContext.Homes.FindAsync(id);
    }

    public async Task<Home> UpdateAsync(long id, Home entity)
    {
        var existingHome = await _rentalFlowDbContext.Homes.FindAsync(id);

        if (existingHome == null)
        {
            throw new KeyNotFoundException($"Home with ID {id} not found.");
        }
        existingHome.HostId = entity.HostId;
        existingHome.Address = entity.Address;
        existingHome.Description = entity.Description;
        existingHome.IsAvailable = entity.IsAvailable;
        existingHome.NoofBedRooms = entity.NoofBedRooms;
        existingHome.NoofBathRooms = entity.NoofBathRooms;
        existingHome.Area = entity.Area;
        existingHome.IsPetAllowed = entity.IsPetAllowed;
        existingHome.HomeType = entity.HomeType;
        existingHome.Price = entity.Price;

        _rentalFlowDbContext.Homes.Update(existingHome);
        await _rentalFlowDbContext.SaveChangesAsync();
        return existingHome;
    }
}
