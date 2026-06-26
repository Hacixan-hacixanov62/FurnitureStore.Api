using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Service.DTO.Admin.BlogCategory
{
    public class BlogCategoryCreateDto
    {
        public string Name { get; set; }
    }

    public class BlogCategoryCreateDtoValidator : AbstractValidator<BlogCategoryCreateDto>
    {
        public BlogCategoryCreateDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required")
                                .MaximumLength(50).WithMessage("Name can be max 50 characters");           
        }
    }
}
