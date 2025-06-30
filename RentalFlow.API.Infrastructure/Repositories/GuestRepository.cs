using Microsoft.EntityFrameworkCore;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Infrastructure.Persistance;

namespace RentalFlow.API.Infrastructure.Repositories;

public class GuestRepository : IGenericRepository<Guest>
{
    private readonly RentalFlowDbContext _rentalFlowDbContext;
    public GuestRepository(RentalFlowDbContext rentalFlowDbContext)
    {
        _rentalFlowDbContext = rentalFlowDbContext;
    }
    public async Task<Guest> CreateAsync(Guest entity)
    {
        await _rentalFlowDbContext.Guests.AddAsync(entity);
        await _rentalFlowDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<long> DeleteAsync(long id)
    {
        var guests =  await _rentalFlowDbContext.Guests.FindAsync(id);
        if (guests == null)
        {
            throw new KeyNotFoundException($"Guest with ID {id} not found.");
        }

        _rentalFlowDbContext.Guests.Remove(guests);
        await _rentalFlowDbContext.SaveChangesAsync();
        return id;
    }

    public async Task<IEnumerable<Guest>> GetAllAsync()
    {
        return await _rentalFlowDbContext.Guests.ToListAsync();
    }

    public async Task<Guest> GetByIdAsync(long id)
    {
        return await _rentalFlowDbContext.Guests.FindAsync(id);
    }

    public async Task<Guest> UpdateAsync(long id, Guest entity)
    {
        var existingGuest = await _rentalFlowDbContext.Guests.FindAsync(id);
        if (existingGuest == null)
        {
            throw new KeyNotFoundException($"Guest with ID {id} not found.");
        }

        existingGuest.FirstName = entity.FirstName;
        existingGuest.LastName = entity.LastName;
        existingGuest.DateOfBirth = entity.DateOfBirth;
        existingGuest.PhoneNumber = entity.PhoneNumber;
        existingGuest.Gender = entity.Gender;

        _rentalFlowDbContext.Guests.Update(existingGuest);
        await _rentalFlowDbContext.SaveChangesAsync();
        return existingGuest;
    }
}
