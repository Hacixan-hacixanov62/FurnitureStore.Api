using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
