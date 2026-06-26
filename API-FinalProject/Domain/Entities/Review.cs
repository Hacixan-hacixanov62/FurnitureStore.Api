using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public int ProductId { get; set; }  
        public string AppUserId { get; set; }     
        public string Comment { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}
