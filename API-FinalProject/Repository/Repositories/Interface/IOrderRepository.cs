using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetAllWithIncludesAsync();
        Task<List<Order>> GetByUserIdAsync(string userId);
        Task SaveChangesAsync();
    }
}
