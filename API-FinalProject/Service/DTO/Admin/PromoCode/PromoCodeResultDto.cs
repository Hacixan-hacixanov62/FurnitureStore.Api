using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Admin.PromoCode
{
    public class PromoCodeResultDto
    {
        public string Code { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsActive { get; set; }
        public int UsageLimit { get; set; }
        public int UsageCount { get; set; }
    }
}
