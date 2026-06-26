
using Microsoft.AspNetCore.Http;
using Service.DTO.Admin.BlogCategory;

namespace Service.DTO.Admin.BlogPost
{
    public class BlogPostCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HighlightText { get; set; }
        public int BlogCategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
