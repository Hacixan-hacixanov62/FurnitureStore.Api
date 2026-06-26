using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.Brand;
using Service.DTO.Admin.Category;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BrandCreateDto request)
        {
            await _brandService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _brandService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] BrandEditDto request)
        {
            await _brandService.EditAsync(request, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _brandService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandProductCounts()
        {
            var categoryCounts = await _brandService.GetBrandProductCountsAsync();
            return Ok(categoryCounts);
        }
    }
}
