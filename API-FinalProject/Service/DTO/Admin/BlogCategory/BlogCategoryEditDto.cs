using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Service.DTO.Admin.BlogCategory
{
    public class BlogCategoryEditDto
    {
        public string Name { get; set; }
    }
    public class BlogCategoryEditDtoValidator : AbstractValidator<BlogCategoryEditDto>
    {
        public BlogCategoryEditDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required")
                                .MaximumLength(50).WithMessage("Name can be max 50 characters");
        }
    }
}
