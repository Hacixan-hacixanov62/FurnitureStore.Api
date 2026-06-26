
using System.Text.Json.Serialization;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Service.DTOs.Admin.Products
{
    public class ProductEditDto
    {
        public string Name { get; set; }
            public int Stock { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int BrandId { get; set; }
            public int CategoryId { get; set; }
            public List<int> TagIds { get; set; } = new();
            public List<int> ColorIds { get; set; } = new();

            public List<IFormFile> UploadImages { get; set; } = new(); 
            public int? MainImageId { get; set; } 

            [JsonIgnore]
            public ICollection<ProductImage> ProductImages { get; set; } = []; 

    }
}
