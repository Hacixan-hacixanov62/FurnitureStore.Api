using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }   
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
