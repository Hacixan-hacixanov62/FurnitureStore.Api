using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Service.DTO.Admin.Color
{
    public class ColorEditDto
    {
        public string Name { get; set; }
    }
    public class ColorEditDtoValidator : AbstractValidator<ColorEditDto>
    {
        public ColorEditDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required").MaximumLength(15);               
        }
    }
}
