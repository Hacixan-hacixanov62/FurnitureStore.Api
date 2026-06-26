using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.UI.Wishlists;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    [Authorize(Roles = "SuperAdmin")]

    public class WishlistController : BaseController
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWishlist([FromQuery]int id)
        {
            await _wishlistService.DeleteWishlistAsync(id);
            return NoContent();
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishlistByUserId([FromRoute] string userId)
        {
            var wishlist = await _wishlistService.GetByUserIdAsync(userId);
            if (wishlist == null) throw new KeyNotFoundException("User not found");
            return Ok(wishlist);
        }
    }
}
