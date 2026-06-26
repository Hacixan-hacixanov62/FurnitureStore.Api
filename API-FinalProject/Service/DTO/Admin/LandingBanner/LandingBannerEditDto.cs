using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.LandingBanner
{
    public class LandingBannerEditDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class LandingBannerEditDtoValidator : AbstractValidator<LandingBannerEditDto>
    {
        public LandingBannerEditDtoValidator()
        {
            RuleFor(s => s.Title).NotEmpty().MaximumLength(100);
            RuleFor(s => s.Description).NotEmpty().MaximumLength(250);
            RuleFor(m => m.Image).Must(m => m == null || m.ContentType.Contains("image/"))
                                 .WithMessage("File must be image type");
        }
    }
}
