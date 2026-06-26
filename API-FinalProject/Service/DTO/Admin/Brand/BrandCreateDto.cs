using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.Brand
{
    public class BrandCreateDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
    public class BrandCreateDtoValidator : AbstractValidator<BrandCreateDto>
    {
        public BrandCreateDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required").MaximumLength(40);

            RuleFor(m => m.Image).NotNull().WithMessage("Image is required")
                 .Must(p => p.ContentType.Contains("image/")).When(m => m.Image is not null)
                 .WithMessage("File must be image type")
                 .Must(p => p.Length / 1024 < 500).WithMessage("Image size cannot exceed 500Kb")
                 .When(m => m.Image is not null);
        }
    }
}
