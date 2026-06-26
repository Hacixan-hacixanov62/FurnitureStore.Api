using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.BlogCategory;

namespace Service.DTO.Admin.BlogPost
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HighlightText { get; set; }
        public int BlogCategoryId { get; set; }
        public string BlogCategory { get; set; }
        public List<BlogPostImgDto> Images { get; set; }
    }
}
