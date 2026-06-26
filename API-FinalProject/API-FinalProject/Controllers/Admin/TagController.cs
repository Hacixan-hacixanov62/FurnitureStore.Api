using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.BlogCategory;
using Service.DTO.Admin.Tag;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class TagController : BaseController
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TagCreateDto request)
        { 
          await _tagService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tagService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _tagService.GetByIdAsync(id);

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] TagEditDto model)
        {
            await _tagService.EditAsync(model, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _tagService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas([FromQuery] int page, [FromQuery] int take)
        {
            return Ok(await _tagService.GetPaginateAsync(page, take));

        }
    }
}
