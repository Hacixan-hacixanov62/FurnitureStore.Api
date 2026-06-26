using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Admin.PromoCode
{
    public class PromoCodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int DiscountPercent { get; set; }
        public int UsageLimit { get; set; }
        public bool IsActive { get; set; }
    }
}
