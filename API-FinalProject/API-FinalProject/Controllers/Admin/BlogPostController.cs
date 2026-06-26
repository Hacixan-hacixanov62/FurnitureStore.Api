using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.BlogCategory;
using Service.DTO.Admin.BlogPost;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class BlogPostController : BaseController
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogPostCreateDto model)
        {
             await _blogPostService.CreateAsync(model);
             return CreatedAtAction(nameof(Create), "Blog Post created success.");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _blogPostService.DeleteAsync(id);
            return Ok(new { message = "Blog post deleted successfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _blogPostService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            return Ok(await _blogPostService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromForm] BlogPostEditDto model)
        {
           await _blogPostService.EditAsync(id, model);
            return Ok(new { message = "Blog post updated successfully!" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromQuery]int blogPostId,[FromQuery] int blogPostImageId)
        {
            bool isDeleted = await _blogPostService.DeleteImageAsync(blogPostId, blogPostImageId);
            if (!isDeleted)
            {
                return NotFound(new { message = "Image not found or blog post does not exist." });
            }

            return Ok(new { message = "Image deleted successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas([FromQuery] int page, [FromQuery] int take)
        {
            return Ok(await _blogPostService.GetPaginateAsync(page, take));
        }
    }
}
