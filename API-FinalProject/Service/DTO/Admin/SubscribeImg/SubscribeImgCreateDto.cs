using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Service.DTO.Admin.Sliders;

namespace Service.DTO.Admin.SubscribeImg
{
    public class SubscribeImgCreateDto
    {
        public IFormFile Img { get; set; }
    }

    public class SubscribeImgCreateDtoValidator : AbstractValidator<SubscribeImgCreateDto>
    {
        public SubscribeImgCreateDtoValidator()
        {          
            RuleFor(m => m.Img).NotEmpty();
            RuleFor(m => m.Img).Must(m => m.ContentType.Contains("image/"))
                                 .WithMessage("File must be image type");
        }
    }
}
