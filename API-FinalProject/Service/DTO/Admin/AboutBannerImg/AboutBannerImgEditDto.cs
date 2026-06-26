using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.AboutBannerImg
{
    public class AboutBannerImgEditDto
    {
        public IFormFile Img { get; set; }
    }
    public class AboutBannerImgEditDtoValidator : AbstractValidator<AboutBannerImgEditDto>
    {
        public AboutBannerImgEditDtoValidator()
        {
            RuleFor(m => m.Img).NotEmpty();
            RuleFor(m => m.Img).Must(m => m.ContentType.Contains("image/"))
                               .WithMessage("File must be image type");
        }
    }
}
