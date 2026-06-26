using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Service.DTO.Admin.Color
{
    public class ColorCreateDto
    {
        public string Name { get; set; }
    }
    public class ColorCreateDtoValidator : AbstractValidator<ColorCreateDto>
    {
        public ColorCreateDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required").MaximumLength(15);
        }
    }
}
