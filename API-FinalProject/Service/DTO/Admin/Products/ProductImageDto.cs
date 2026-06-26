using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Products
{
	public class ProductImageDto
	{
		public int Id { get; set; }
		public string Img { get; set; }
		public bool IsMain { get; set; }
	}

    public class ProductWithImagesDto
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string Brand { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public List<string> Tags { get; set; }
        public List<string> Colors { get; set; }
        public List<ProductImageDto> ProductImages { get; set; }
    }
}
