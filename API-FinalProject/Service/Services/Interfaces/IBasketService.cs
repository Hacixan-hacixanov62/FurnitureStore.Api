using Service.DTO.UI.Basket;
using Service.DTOs.UI.Basket;

namespace Service.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddBasketAsync(BasketCreateDto basketCreateDto);
        Task<List<BasketDto>> GetAllBasketsAsync();
        Task<BasketDto> GetBasketByUserIdAsync(string userId);
        Task IncreaseQuantityAsync(BasketCreateDto basketCreateDto);
        Task DecreaseQuantityAsync(BasketCreateDto basketCreateDto);    
        Task DeleteProductFromBasketAsync(int productId, string userId);
        Task DeleteProductByUserIdAsync(string userId);
        Task<List<BasketItemDto>> GetLastTwoProductsAsync(string userId);
    }
}
