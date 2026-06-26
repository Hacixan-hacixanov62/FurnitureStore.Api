using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service.DTO.Admin.Order
{
    public class ChangeOrderStatusDto
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}
