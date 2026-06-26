using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.UI.Wishlists;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Client
{
    public class WishlistController : BaseController
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;

        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishlistByUserId([FromRoute] string userId)
        {
            var wishlist = await _wishlistService.GetByUserIdAsync(userId);
            if (wishlist == null) throw new KeyNotFoundException("User not found");
            return Ok(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddWishlist([FromQuery] string appUserId, [FromQuery] int productId)
        {
            var wishlist = new WishlistDto { AppUserId = appUserId, ProductId = productId };
            var result = await _wishlistService.AddWishlistAsync(wishlist);

            return Ok(result); 
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProductFromWishlist([FromRoute] string userId, [FromQuery] int productId)
        {
            await _wishlistService.DeleteProductFromWishList(productId, userId);
            return NoContent();
        }

    }
}