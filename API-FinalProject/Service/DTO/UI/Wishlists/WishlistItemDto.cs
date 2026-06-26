
using Service.DTOs.UI.Wishlists;

namespace Service.DTO.UI.Wishlists
{
    public class WishlistItemDto
    {
        public string AppUserId { get; set; } 
        public List<WishlistProductDto> Products { get; set; }
        public int ProductCount { get; set; }
    }
}
