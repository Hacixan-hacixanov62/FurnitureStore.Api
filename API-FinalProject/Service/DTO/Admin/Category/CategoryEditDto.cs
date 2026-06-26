using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTO.Admin.Category
{
    public class CategoryEditDto
    {
        public string Name { get; set; } 
        public IFormFile? Image { get; set; }
    }

    public class CategoryEditDtoValidator : AbstractValidator<CategoryEditDto>
    {
        public CategoryEditDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required").MaximumLength(40);

            RuleFor(m => m.Image).Must(m => m == null || m.ContentType.Contains("image/"))
                                  .WithMessage("File must be image type");
        }
    }
}
