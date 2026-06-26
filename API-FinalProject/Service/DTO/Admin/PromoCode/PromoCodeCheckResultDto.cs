using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Admin.PromoCode
{
    public class PromoCodeCheckResultDto
    {
        public bool IsValid { get; set; }
        public int DiscountPercent { get; set; }
        public string? Message { get; set; }
    }
}
