using Service.DTO.Admin.Category;
using Service.DTO.Admin.Color;
using Service.DTO.Admin.Tag;

namespace Service.DTOs.Admin.Products
{
	public class ProductDetailDto
	{
		public int Id { get; set; }
		public int Stock { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string Brand { get; set; }
		public IEnumerable<ProductImageDto> ProductImages { get; set; }
		public IEnumerable<CategoryDto> Categories { get; set; }
		public IEnumerable<TagDto> Tags { get; set; }
		public IEnumerable<ColorDto> Colors { get; set; }
	}
}
