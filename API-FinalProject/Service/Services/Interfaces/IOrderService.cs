
using Domain.Entities;
using Service.DTO.Admin.Order;

namespace Service.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderFromBasketAsync(OrderCreateDto dto);
        Task<List<OrderDto>> GetAllAsync();
        Task<List<OrderDto>> GetByUserIdAsync(string userId);
        Task ChangeStatusAsync(int orderId, string newStatus);

    }
}
