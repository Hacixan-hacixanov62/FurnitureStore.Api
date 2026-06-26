
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class BlogReviewController : BaseController
    {
        private readonly IBlogReviewService _reviewService;

        public BlogReviewController(IBlogReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview([FromQuery] int id)
        {
            if (id == 0) return BadRequest("Id is required.");
            await _reviewService.DeleteAsync(id);
            return Ok($"Delete review user with id: {id}");
        }
    }
}
