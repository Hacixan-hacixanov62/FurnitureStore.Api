using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Service.DTO.Admin.SubscribeImg;

namespace Service.DTO.Admin.Instagram
{
    public class InstagramCreateDto
    {
        public IFormFile Img { get; set; }    
    }
    public class InstagramCreateDtoValidator : AbstractValidator<InstagramCreateDto>
    {
        public InstagramCreateDtoValidator()
        {
            RuleFor(m => m.Img).NotEmpty();
            RuleFor(m => m.Img).Must(m => m.ContentType.Contains("image/"))
                               .WithMessage("File must be image type");
        }
    }
}
