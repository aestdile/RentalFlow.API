using Microsoft.EntityFrameworkCore;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Infrastructure.Persistance;

namespace RentalFlow.API.Infrastructure.Repositories;

public class HostRepository : IGenericRepository<Host>
{
    private readonly RentalFlowDbContext _rentalFlowDbContext;
    public HostRepository(RentalFlowDbContext rentalFlowDbContext)
    {
        _rentalFlowDbContext = rentalFlowDbContext;
    }
    public async Task<Host> CreateAsync(Host entity)
    {
        await _rentalFlowDbContext.Hosts.AddAsync(entity);
        await _rentalFlowDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<long> DeleteAsync(long id)
    {
        var host = await _rentalFlowDbContext.Hosts.FindAsync(id);
        if (host == null)
        {
            throw new KeyNotFoundException($"Host with ID {id} not found.");
        }

        _rentalFlowDbContext.Hosts.Remove(host);
        await _rentalFlowDbContext.SaveChangesAsync();
        return id;
    }

    public async Task<IEnumerable<Host>> GetAllAsync()
    {
        return await _rentalFlowDbContext.Hosts.ToListAsync();
    }

    public async Task<Host> GetByIdAsync(long id)
    {
        return await _rentalFlowDbContext.Hosts.FindAsync(id);
    }

    public async Task<Host> UpdateAsync(long id, Host entity)
    {
        var existingHost =await _rentalFlowDbContext.Hosts.FindAsync(id);
        if (existingHost == null)
        {
            throw new KeyNotFoundException($"Host with ID {id} not found.");
        }

        existingHost.FirstName = entity.FirstName;
        existingHost.LastName = entity.LastName;
        existingHost.DateOfBirth = entity.DateOfBirth;
        existingHost.PhoneNumber = entity.PhoneNumber;
        existingHost.Gender = entity.Gender;

        _rentalFlowDbContext.Hosts.Update(existingHost);
        await _rentalFlowDbContext.SaveChangesAsync();
        return existingHost;
    }
}
