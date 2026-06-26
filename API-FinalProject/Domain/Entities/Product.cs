using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Stock { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<WishlistProduct> WishlistProducts { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
