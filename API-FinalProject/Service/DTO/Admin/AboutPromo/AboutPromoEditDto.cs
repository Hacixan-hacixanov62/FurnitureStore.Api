using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.AboutPromo
{
    public class AboutPromoEditDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
