using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class PromoCode : BaseEntity
    {
        [Required]
        public string Code { get; set; } = null!;

        [Range(1, 100)]
        public int DiscountPercent { get; set; }
        public bool IsActive { get; set; } = true;

        [Range(0, int.MaxValue)]
        public int UsageLimit { get; set; } = 0;
        public int UsageCount { get; set; } = 0;
    }
}
