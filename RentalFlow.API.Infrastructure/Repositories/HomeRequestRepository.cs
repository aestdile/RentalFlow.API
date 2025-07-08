using Microsoft.EntityFrameworkCore;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Infrastructure.Persistance;

namespace RentalFlow.API.Infrastructure.Repositories;

public class HomeRequestRepository : IGenericRepository<HomeRequest>
{
    private readonly RentalFlowDbContext _rentalFlowDbContext;
    public HomeRequestRepository(RentalFlowDbContext rentalFlowDbContext)
    {
        _rentalFlowDbContext = rentalFlowDbContext;
    }
    public async Task<HomeRequest> CreateAsync(HomeRequest entity)
    {
        await _rentalFlowDbContext.HomeRequests.AddAsync(entity);
        await _rentalFlowDbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<long> DeleteAsync(long id)
    {
        var homeRequest = await _rentalFlowDbContext.HomeRequests.FindAsync(id);
        if (homeRequest == null)
            return 0;

        _rentalFlowDbContext.HomeRequests.Remove(homeRequest);
        await _rentalFlowDbContext.SaveChangesAsync();
        return id;
    }

    public async Task<IEnumerable<HomeRequest>> GetAllAsync()
    {
        return await _rentalFlowDbContext.HomeRequests.ToListAsync();
    }
    public async Task<HomeRequest> GetByIdAsync(long id)
    {
        return await _rentalFlowDbContext.HomeRequests.FindAsync(id);
    }
    public async Task<HomeRequest> UpdateAsync(long id, HomeRequest entity)
    {
        var existingHomeRequest = await _rentalFlowDbContext.HomeRequests.FindAsync(id);
        if (existingHomeRequest == null)
        {
            throw new KeyNotFoundException($"HomeRequest with ID {id} not found.");
        }

        existingHomeRequest.RequestMessage = entity.RequestMessage;
        existingHomeRequest.StartDate = entity.StartDate;
        existingHomeRequest.EndDate = entity.EndDate;
        existingHomeRequest.UpdatedAt = DateTime.UtcNow;

        _rentalFlowDbContext.HomeRequests.Update(existingHomeRequest);
        await _rentalFlowDbContext.SaveChangesAsync();
        return existingHomeRequest;
    }
}
