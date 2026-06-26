using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HighlightText { get; set; }
        public int BlogCategoryId { get; set; } 
        public BlogCategory BlogCategory { get; set; } 

        public ICollection<BlogPostImg> Images { get; set; }
        public ICollection<BlogReview> BlogReviews { get; set; }
    }
}
