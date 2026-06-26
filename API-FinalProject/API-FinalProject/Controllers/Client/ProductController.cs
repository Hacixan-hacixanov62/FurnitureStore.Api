using API_FinalProject.Controllers.Client;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.Products;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.UI
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

		[HttpGet("{id}")]
		public async Task<IActionResult> ProductDetail([FromRoute] int id)
		{
			var product = await _productService.DetailAsync(id);

			if (product is null) return NotFound();

			return Ok(product);
		}

		[HttpGet]
		public async Task<IActionResult> SearchByName([FromQuery] string name)
		{
			return Ok(await _productService.SearchByCategoryAndName(name));
		}


        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] string? categoryName,[FromQuery] string? colorName,
                                                [FromQuery] string? tagName, [FromQuery] string? brandName)
        {
            return Ok(await _productService.FilterAsync(categoryName, colorName, tagName, brandName));
        }


        [HttpGet("take")]
        public async Task<IActionResult> GetAllTaken([FromQuery] int take, [FromQuery] int? skip = null)
        {
            var products = await _productService.GetAllTakenAsync(take, skip);
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsCount()
        {
            var count = await _productService.GetProductsCountAsync();
            return Ok(count);
        }


        [HttpGet]
        public async Task<IActionResult> GetSortedProducts([FromQuery] string sortType)
        {
            var sortedProducts = await _productService.GetSortedProductsAsync(sortType);
            return Ok(sortedProducts);
        }

        [HttpGet]
        public async Task<IActionResult> GetComparisonProducts([FromQuery]int categoryId, [FromQuery] int selectedProduct)
        {
            if (categoryId <= 0) return BadRequest("Invalid category ID");

            var products = await _productService.GetComparisonProductsAsync(categoryId, selectedProduct);

            if (products == null || !products.Any())
            {
                return NotFound("No products found for the given category");
            }

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByPrice([FromBody] ProductFilterDto filterDto)
        {
            var result = await _productService.FilterByPriceAsync(filterDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxPrice()
        {
            var maxPrice = await _productService.GetMaxPriceAsync();
            return Ok(maxPrice);
        }
    }
}
