namespace RentalFlow.API.Application.Interfaces.Repositories;

public interface IGenericRepository<Entity> where Entity : class
{
    Task<Entity> CreateAsync(Entity entity);
    Task<Entity> GetByIdAsync(long id);
    Task<IEnumerable<Entity>> GetAllAsync();
    Task<Entity> UpdateAsync(long id, Entity entity);
    Task<long> DeleteAsync(long id);
}