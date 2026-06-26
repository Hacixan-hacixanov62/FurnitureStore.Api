using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.UI.Wishlists;
using Service.DTOs.UI.Wishlists;

namespace Service.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<WishlistResult> AddWishlistAsync(WishlistDto wishlistDto);
        Task DeleteWishlistAsync(int id);
        Task DeleteProductFromWishList(int productId, string userId);
        Task<WishlistItemDto> GetByUserIdAsync(string userId);
    }
}
