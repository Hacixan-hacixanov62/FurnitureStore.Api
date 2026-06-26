using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class BasketProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
