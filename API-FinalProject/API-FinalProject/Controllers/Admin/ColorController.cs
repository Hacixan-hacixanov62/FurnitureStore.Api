using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.Color;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class ColorController : BaseController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ColorCreateDto request)
        {
            await _colorService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _colorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _colorService.GetByIdAsync(id));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _colorService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ColorEditDto request)
        {
            await _colorService.EditAsync(request, id);
            return Ok();
        }
    }
}
