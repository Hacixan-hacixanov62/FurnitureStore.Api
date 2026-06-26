using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.Instagram
{
    public class InstagramEditDto
    {
        public IFormFile? Img { get; set; }
    }
    public class InstagramEditDtoValidator : AbstractValidator<InstagramEditDto>
    {
        public InstagramEditDtoValidator()
        {
            RuleFor(m => m.Img).Must(m => m == null || m.ContentType.Contains("image/"))
                                 .WithMessage("File must be image type");
        }
    }

}
