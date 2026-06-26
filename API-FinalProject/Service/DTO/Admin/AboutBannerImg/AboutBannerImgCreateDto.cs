using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Service.DTO.Admin.Instagram;

namespace Service.DTO.Admin.AboutBannerImg
{
    public class AboutBannerImgCreateDto
    {
        public IFormFile Img { get; set; }
    }
    public class AboutBannerImgCreateDtoValidator : AbstractValidator<AboutBannerImgCreateDto>
    {
        public AboutBannerImgCreateDtoValidator()
        {
            RuleFor(m => m.Img).NotEmpty();
            RuleFor(m => m.Img).Must(m => m.ContentType.Contains("image/"))
                               .WithMessage("File must be image type");
        }
    }
}

