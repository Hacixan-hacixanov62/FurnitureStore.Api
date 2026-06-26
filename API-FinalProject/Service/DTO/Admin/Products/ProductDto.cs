using Service.DTO.Admin.Category;
using Service.DTO.Admin.Color;
using Service.DTO.Admin.Tag;

namespace Service.DTOs.Admin.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string MainImage { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public List<string> Images { get; set; }
    }
}
