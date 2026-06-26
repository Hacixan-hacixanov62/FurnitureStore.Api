using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.BlogCategory;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class BlogCategoryController : BaseController
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _blogCategoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _blogCategoryService.GetByIdAsync(id);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogCategoryCreateDto model)
        {
            await _blogCategoryService.CreateAsync(model);
            return CreatedAtAction(nameof(Create), "Created success.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] BlogCategoryEditDto model)
        {
            await _blogCategoryService.EditAsync(model, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _blogCategoryService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryPostCounts()
        {
            var categoryCounts = await _blogCategoryService.GetCategoryPostCountsAsync();
            return Ok(categoryCounts);
        }
    }
}
