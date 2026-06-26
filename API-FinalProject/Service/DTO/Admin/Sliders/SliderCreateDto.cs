using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.Sliders
{
    public class SliderCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }

    public class SliderCreateDtoValidator : AbstractValidator<SliderCreateDto>
    {
        public SliderCreateDtoValidator()
        {
            RuleFor(s => s.Title).NotEmpty().MaximumLength(100);
            RuleFor(s => s.Description).NotEmpty().MaximumLength(250);
            RuleFor(m => m.Image).NotEmpty();
            RuleFor(m => m.Image).Must(m => m.ContentType.Contains("image/"))
                                 .WithMessage("File must be image type");
        }
    }
}
