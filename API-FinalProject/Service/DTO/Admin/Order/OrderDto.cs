using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service.DTO.Admin.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string AppUserEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public string Status { get; set; }
    }

}
