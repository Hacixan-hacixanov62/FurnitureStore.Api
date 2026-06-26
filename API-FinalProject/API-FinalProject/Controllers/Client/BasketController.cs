using API_FinalProject.Controllers.Client;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.UI.Basket;
using Service.Services;
using Service.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinalProject.Controllers.UI
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasketByUserId([FromRoute]string userId)
        {
            var basket = await _basketService.GetBasketByUserIdAsync(userId);
            return Ok(basket);
        }

		[HttpPost]
		public async Task<IActionResult> AddBasket([FromBody] BasketCreateDto basketCreateDto)
		{
			await _basketService.AddBasketAsync(basketCreateDto);
			return Ok();
		}


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteBasketProduct([FromRoute] string userId)
        {
            if (userId == null) return BadRequest();
            await _basketService.DeleteProductByUserIdAsync(userId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity([FromBody] BasketCreateDto basketCreateDto)
        {
            await _basketService.IncreaseQuantityAsync(basketCreateDto);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity([FromBody] BasketCreateDto basketCreateDto)
        {
            await _basketService.DecreaseQuantityAsync(basketCreateDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductFromBasket(int productId, string userId)
        {
            await _basketService.DeleteProductFromBasketAsync(productId, userId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket([FromHeader] string token)
        {
            var userId = GetUserIdFromToken(token);

            if (userId == null) return Unauthorized();

             await _basketService.DeleteProductByUserIdAsync(userId);
            if (userId!=null) return Ok("Basket deleted successfully.");

            return StatusCode(500, "Failed to delete basket.");
        }


        private string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid");

                return userIdClaim?.Value;
            }

            return null;
        }

        [HttpGet("last-two")]
        public async Task<IActionResult> GetLastTwoProducts(string userId)
        {
            var result = await _basketService.GetLastTwoProductsAsync(userId);
            return Ok(result);
        }
    }
}
