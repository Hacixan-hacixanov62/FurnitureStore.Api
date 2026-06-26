using API_FinalProject.Controllers.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Products;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            try
            {
                await _productService.CreateAsync(request);
                return CreatedAtAction(nameof(Create), new { response = "Product successfully created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id == null) return BadRequest();
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ProductEditDto request)
        {
            await _productService.EditAsync(id, request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas([FromQuery] int page, [FromQuery] int take)
        {
            return Ok(await _productService.GetPaginateAsync(page, take));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage([FromForm] int productId, [FromForm] int productImageId)
        {
            bool isDeleted = await _productService.DeleteImageAsync(productId, productImageId);
            if (!isDeleted)
            {
                return NotFound(new { message = "Image not found or product does not exist." });
            }

            return Ok(new { message = "Image deleted successfully." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdWithImages([FromRoute]int id)
        {
            var product = await _productService.GetByIdWithImagesAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            return Ok(await _productService.SearchByName(name));
        }
    }
}