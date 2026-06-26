using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.SubscribeImg
{
    public class SubscribeImgEditDto
    {
        public IFormFile? Img { get; set; }
    }
    public class SubscribeImgEditDtoValidator : AbstractValidator<SubscribeImgEditDto>
    {
        public SubscribeImgEditDtoValidator()
        {
            RuleFor(m => m.Img).Must(m => m == null || m.ContentType.Contains("image/"))
                                       .WithMessage("File must be image type");
        }
    }
}
