using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Admin.Order
{
    public class OrderCreateDto
    {
        public string AppUserId { get; set; }
        public string StripeSessionId { get; set; }
        public string? PromoCode { get; set; }
    }
}
