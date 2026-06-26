using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
