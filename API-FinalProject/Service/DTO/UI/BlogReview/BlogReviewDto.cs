using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.UI.BlogReview
{
    public class BlogReviewDto
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string BlogPostName { get; set; }
        public string AppUserId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
