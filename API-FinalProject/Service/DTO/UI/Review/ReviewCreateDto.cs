using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.UI.Review
{
    public class ReviewCreateDto
    {
        public int ProductId { get; set; }
        public string AppUserId { get; set; } 
        public string Comment { get; set; }
      
    }
}
