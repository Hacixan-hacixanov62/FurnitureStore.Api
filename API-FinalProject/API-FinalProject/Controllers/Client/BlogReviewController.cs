using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.UI.Review;
using Service.Services.Interfaces;
using Service.DTO.UI.BlogReview;
using System.IdentityModel.Tokens.Jwt;

namespace API_FinalProject.Controllers.Client
{
    public class BlogReviewController : BaseController
    {
        private readonly IBlogReviewService _reviewService;
        private readonly UserManager<AppUser> _userManager;

        public BlogReviewController(IBlogReviewService reviewService,
                                UserManager<AppUser> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetReviewsByPost(int postId)
        {
            var reviews = await _reviewService.GetReviewsByPostIdAsync(postId);
            return Ok(reviews);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] BlogReviewCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);

            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
                userEmail = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
                userEmail = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized();

            var success = await _reviewService.CreateReviewAsync(userEmail, dto);
            if (!success)
                return BadRequest("User not found or invalid data.");

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BlogReviewEditDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);

            var result = await _reviewService.EditReviewAsync(id, dto);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email); 
            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized();

            var success = await _reviewService.DeleteReviewAsync(userEmail, id);
            if (!success)
                return BadRequest("Review not found or you don't have permission.");

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByPostId(int postId)
        {
            var reviews = await _reviewService.GetAllByPostIdAsync(postId);
            return Ok(reviews ?? Enumerable.Empty<BlogReviewDto>());
        }
    }
}
